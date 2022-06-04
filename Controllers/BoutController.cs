using Boat_2.Data;
using Boat_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boat_2.Controllers;

[Route("Api/Boat")]
[Helpers.Authorize]
[ApiController]
public class BoatController : ControllerBase
{
    private readonly AppDbContext _context;

    public BoatController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Boat))]
    public async Task<List<Boat>> Boats(int boatId, BoatStatus status,int PageNumber,int PageSize)
    {
        List<Boat> boats = _context.Boats.ToList();
        if (PageSize != 0 && PageNumber != 0) boats = await Pagination<Boat>.CreateAsync(_context.Boats, PageNumber, PageSize);
        if (status != 0) boats = boats.Where(b => b.Status == status.ToString()).ToList();
        if (boatId != 0) boats = boats.Where(b => b.Id == boatId).ToList();
        return boats;
    }

    [HttpPost("/Add")]
    public Task<Boat> AddBoat(string name, BoatStatus boatStatus,int crewSeats ,string crews, string boutQualification)
    {
        Boat newBoat = new();
        newBoat.Name = name;
        newBoat.QualificationNeed = CheckQualification(boutQualification);
        newBoat.CrewSeats = crewSeats;
        newBoat.Status = boatStatus.ToString();
        _context.Boats.Add(newBoat);
        _context.SaveChanges();
        return Task.FromResult(newBoat);
    }


    [HttpPost("/Update")]
      public async Task<ActionResult<string>> UpdateBoat(int id,string name,BoatStatus status,int CrewSeats,PersonQualification qualification)
      {
          string result = string.Empty;
          Boat? Boat = await _context.Boats.FindAsync(id);

          if (Boat == null)
          {
              return BadRequest();
          }

          var statusEnums = Enum.GetValues(typeof(BoatStatus))
              .Cast<BoatStatus>()
              .Select(v => v.ToString())
              .ToList();

         if (Boat.Status != status.ToString() && statusEnums.Contains(status.ToString())) { Boat.Status = status.ToString(); }

          System.Diagnostics.Debug.WriteLine("Gamoitanos -" + statusEnums.Contains(status.ToString()));

          if (Boat.CrewSeats != 0 || Boat.CrewSeats != CrewSeats) { Boat.CrewSeats = CrewSeats; }

          if (name != null) Boat.Name = name;
          if (qualification != 0) Boat.QualificationNeed = qualification.ToString();
          _context.SaveChanges();
          return Ok(string.Format("Updated {0}",Boat) );
      }


    [HttpPost("AddCrew/{personId}/BoutId")]
    public async Task<ActionResult<Boat>> AddCrews(int personId,int BoatId)
    {
        var BoatCrew = await _context.Boats.FirstOrDefaultAsync(b => b.Id == BoatId);
        var Crew = await _context.Persons.FirstOrDefaultAsync(pe => pe.Id == personId);
        if (BoatCrew == null)
            return BadRequest(string.Format("Not Found Boat With Id {0}",BoatId)); ;
        if (Crew == null)
            return BadRequest(string.Format("Not Found Person With Id {0}", personId));
        if (BoatCrew.QualificationNeed.Contains(Crew.Qualification))
        {
            return BadRequest(string.Format("Persons qualification is {0} and need {1}",Crew.Qualification,BoatCrew.QualificationNeed));
        }
        List<string> bCrews = BoatCrew.Crews.Split(new char[] {','}).ToList();
        if (BoatCrew.CrewSeats > bCrews.Count && !bCrews.Contains(personId.ToString()))
            BoatCrew.Crews += String.Format("{0},",Crew.Id);
        else
            return BadRequest("There are no more seats on the boat or This Person Already on the boat ");
        if (Crew.BoatId != 0)
        {
            var oldBoat = await _context.Boats.FirstOrDefaultAsync(b => b.Id == Crew.BoatId);
            if (oldBoat!=null) oldBoat.Crews = oldBoat.Crews.Replace(string.Format("{0},", Crew.Id), "");
        }
        Crew.BoatId = BoatCrew.Id;
        _context.SaveChanges();
        return Ok(BoatCrew.Crews.Split(new char[] {','}).ToList());
    }

    //Helpers
    private string? CheckQualification(string ComparableQuality = "", string CrewsId = "")
    {
        List<string> returnString = new List<string> {""};
        List<string> DefaultQualifications = Enum.GetValues(typeof(PersonQualification))
            .Cast<PersonQualification>()
            .Select(v => v.ToString())
            .ToList();
        List<string> BoatQuality = ComparableQuality.Split(',').ToList();
        if (ComparableQuality != "" && CrewsId == "")
        {
            foreach (string quality in BoatQuality)
            {
                if (DefaultQualifications.Contains(quality))
                {
                    returnString.Add(quality);
                }
            }
        } else if (ComparableQuality != null && CrewsId != null)
        {
            List<Person> persons = _context.Persons.ToList();
            
            foreach(string quality in BoatQuality)
            {

            }

        }
        System.Diagnostics.Debug.WriteLine("------------------"+ string.Join(",", returnString));
        return string.Join(",", returnString);
    }
}
