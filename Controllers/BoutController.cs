using Bout_2.Data;
using Bout_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bout_2.Controllers
{
    [Route("Api/")]
    [ApiController]
    public class BoutController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BoutController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("bout")]
        public Task<List<Bout>> Bouts()
        {
            List<Bout> bouts = _context.Bouts.ToList();
            return Task.FromResult(bouts);
        }


        [HttpGet("Get/Page:{page}/Element:{elementNumber}/all")]
        public List<Bout> GetPageByStatus(int page, int elementNumber)
        {
            List<Bout> bouts = _context.Bouts.ToList();

            if (page == 1 && page == 0)
            {
                List<Bout> GetBouts = new(bouts.Take(elementNumber));
                return GetBouts;
            }
            else
            {
                List<Bout> GetBouts = new(bouts.Skip(page * elementNumber).Take(elementNumber));
                return GetBouts;
            }
        }

        [HttpGet("Get/Page:{page}/Element:{elementNumber}/Status:{status}")]
        public List<Bout> GetPageByStatus(int page, int elementNumber, BoutStatus status)
        {
            List<Bout> bouts = _context.Bouts.Where(b => b.Status == status.ToString()).ToList();

            if (page == 1 && page == 0)
            {
                List<Bout> GetBouts = new(bouts.Take(elementNumber));
                return GetBouts;
            }
            else
            {
                List<Bout> GetBouts = new(bouts.Skip(page * elementNumber).Take(elementNumber));
                return GetBouts;
            }
        }

        [HttpPost("/Add/{name}/{BoutStatus}/{CovidVaccineNeed}/{MaxWeightPerPessenger}/{NumberOfSeats}")]
        public Task<Bout> AddBout(string name, BoutStatus BoutStatus, bool CovidVaccineNeed, int MaxWeightPerPessenger, int NumberOfSeats)
        {
            Bout newBout = new();
            newBout.Name = name;
            newBout.CovidVaccineNeed = CovidVaccineNeed;
            newBout.MaxWeightPerPessenger = MaxWeightPerPessenger;
            newBout.NumberOfSeats = NumberOfSeats;
            newBout.Pessengers = "";
            newBout.Status = BoutStatus.ToString();
            _context.Bouts.Add(newBout);
            _context.SaveChanges();
            return Task.FromResult(newBout);
        }

        [HttpPost("/Update/{id}/{name}/{status}/{covidVaccineNeed}/{numberOfSeats}/{weightPerPessenger}")]
        public async Task<ActionResult<string>> UpdateBout(int id,string name,BoutStatus status,bool covidVaccineNeed,int numberOfSeats,double weightPerPessenger)
        {
            string result = string.Empty;
            Bout? updatedBout = (from c in _context.Bouts
                                 where c.Id == id
                                 select c).FirstOrDefault();
            if (updatedBout == null)
            {
                return BadRequest();
            }

            var statusEnums = Enum.GetValues(typeof(BoutStatus))
                .Cast<BoutStatus>()
                .Select(v => v.ToString())
                .ToList();

           if (updatedBout.Status != status.ToString() && statusEnums.Contains(status.ToString())) { updatedBout.Status = status.ToString(); }
            else
            {
                result += ", Status not Changed, ";
            }
            System.Diagnostics.Debug.WriteLine("Gamoitanos -" + statusEnums.Contains(status.ToString()));
            if (updatedBout.NumberOfSeats != 0 || updatedBout.NumberOfSeats != numberOfSeats) { updatedBout.NumberOfSeats = numberOfSeats; } else { result += "Number of Seats Not Changed "; }

            if (name!=null) updatedBout.Name = name;
            if (weightPerPessenger != 0)  updatedBout.MaxWeightPerPessenger = weightPerPessenger;
            updatedBout.CovidVaccineNeed = covidVaccineNeed;
            _context.SaveChanges();
            return Ok(string.Format("Updated {0} {1}",updatedBout , result) );
        }
        
        
     /*   [HttpPost()]
        public async Task<ActionResult<Bout>> AddPessengers(Person id)
        {

        }*/

        [HttpGet("/Persons")]
        public Task<List<Person>> People()
        {
            List<Person> persons = _context.Persons.ToList();
            return Task.FromResult(persons);
        }
    }
}
