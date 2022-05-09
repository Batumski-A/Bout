using Boat_2.Data;
using Boat_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boat_2.Controllers
{
    [Route("Api/")]
    [ApiController]
    public class BoatController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BoatController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Boat")]
        public Task<List<Boat>> Boats()
        {
            List<Boat> Boats = _context.Boats.ToList();
            return Task.FromResult(Boats);
        }


        [HttpGet("Get/Page:{page}/Element:{elementNumber}/all")]
        public List<Boat> GetPageByStatus(int page, int elementNumber)
        {
            List<Boat> Boats = _context.Boats.ToList();

            if (page == 1 && page == 0)
            {
                List<Boat> GetBoats = new(Boats.Take(elementNumber));
                return GetBoats;
            }
            else
            {
                List<Boat> GetBoats = new(Boats.Skip(page * elementNumber).Take(elementNumber));
                return GetBoats;
            }
        }

        [HttpGet("Get/Page:{page}/Element:{elementNumber}/Status:{status}")]
        public List<Boat> GetPageByStatus(int page, int elementNumber, BoatStatus status)
        {
            List<Boat> Boats = _context.Boats.Where(b => b.Status == status.ToString()).ToList();

            if (page == 1 || page == 0)
                return new(Boats.Take(elementNumber));
            else
                return new(Boats.Skip(page * elementNumber).Take(elementNumber));
            
        }

        [HttpPost("/Add/{name}/{BoatStatus}/{CovidVaccineNeed}/{MaxWeightPerPessenger}/{NumberOfSeats}")]
        public Task<Boat> AddBoat(string name, BoatStatus BoatStatus, bool CovidVaccineNeed, int MaxWeightPerPessenger, int NumberOfSeats)
        {
            Boat newBoat = new();
            newBoat.Name = name;
            newBoat.CovidVaccineNeed = CovidVaccineNeed;
            newBoat.MaxWeightPerPessenger = MaxWeightPerPessenger;
            newBoat.NumberOfSeats = NumberOfSeats;
            newBoat.Pessengers = "";
            newBoat.Status = BoatStatus.ToString();
            _context.Boats.Add(newBoat);
            _context.SaveChanges();
            return Task.FromResult(newBoat);
        }

        //chasamatebelia
        [HttpPost("/Update/{id}/{name}/{status}/{covidVaccineNeed}/{numberOfSeats}/{weightPerPessenger}")]
        public async Task<ActionResult<string>> UpdateBoat(int id,string name,BoatStatus status,bool covidVaccineNeed,int numberOfSeats,double weightPerPessenger)
        {
            string result = string.Empty;
            Boat? updatedBoat = (from c in _context.Boats
                                 where c.Id == id
                                 select c).FirstOrDefault();
            if (updatedBoat == null)
            {
                return BadRequest();
            }

            var statusEnums = Enum.GetValues(typeof(BoatStatus))
                .Cast<BoatStatus>()
                .Select(v => v.ToString())
                .ToList();

           if (updatedBoat.Status != status.ToString() && statusEnums.Contains(status.ToString())) { updatedBoat.Status = status.ToString(); }
            else
            {
                result += ", Status not Changed, ";
            }
            System.Diagnostics.Debug.WriteLine("Gamoitanos -" + statusEnums.Contains(status.ToString()));
            if (updatedBoat.NumberOfSeats != 0 || updatedBoat.NumberOfSeats != numberOfSeats) { updatedBoat.NumberOfSeats = numberOfSeats; } else { result += "Number of Seats Not Changed "; }

            if (name != null) updatedBoat.Name = name;
            else result += " Name not Changed,";
            if (weightPerPessenger != 0) updatedBoat.MaxWeightPerPessenger = weightPerPessenger;
            else result += " Pessenger not Changed,";
            updatedBoat.CovidVaccineNeed = covidVaccineNeed;
            _context.SaveChanges();
            return Ok(string.Format("Updated {0} {1}",updatedBoat , result) );
        }


        [HttpPost("Bout/AddPessenger/{personId}/BoutId")]
        public async Task<ActionResult<Boat>> AddPessengers(int personId,int BoatId)
        {
            var BoatPessenger = await _context.Boats.FirstOrDefaultAsync(b => b.Id == BoatId);
            var pessenger = await _context.Persons.FirstOrDefaultAsync(pe => pe.Id == personId);
            if (BoatPessenger == null)
                return BadRequest(string.Format("Not Found Boat With Id {0}",BoatId)); ;
            if (pessenger == null)
                return BadRequest(string.Format("Not Found Person With Id {0}", personId));
            if (pessenger.Vaccinated != BoatPessenger.CovidVaccineNeed || pessenger.Weight > BoatPessenger.MaxWeightPerPessenger)
            {
                return BadRequest(string.Format("Need under {0} weight and Vaction {1}", BoatPessenger.MaxWeightPerPessenger,BoatPessenger.CovidVaccineNeed));
            }
            List<string> bPessengers = BoatPessenger.Pessengers.Split(new char[] {','}).ToList();
            if (BoatPessenger.NumberOfSeats > bPessengers.Count && !bPessengers.Contains(personId.ToString()))
                BoatPessenger.Pessengers += String.Format("{0},",pessenger.Id);
            else
                return BadRequest("There are no more seats on the boat or This Person Already on the boat ");
            if (pessenger.BoatId != 0)
            {
                var oldBoat = await _context.Boats.FirstOrDefaultAsync(b => b.Id == pessenger.BoatId);
                oldBoat.Pessengers = oldBoat.Pessengers.Replace(string.Format("{0},", pessenger.Id), "");
            }
            pessenger.BoatId = BoatPessenger.Id;
            _context.SaveChanges();
            return Ok(BoatPessenger.Pessengers.Split(new char[] {','}).ToList());
        }

        [HttpGet("/Persons")]
        public Task<List<Person>> People()
        {
            List<Person> persons = _context.Persons.ToList();
            return Task.FromResult(persons);
        }
    }
}
