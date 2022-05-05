using Bout_2.Data;
using Bout_2.Models;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("/Add")]
        public Task<Bout> AddBout(string name,string boutStatus,bool CovidVaccineNeed,int MaxWeightPerPessenger,int NumberOfSeats)
        {
            Bout newBout = new Bout();
            newBout.Name = name;
            newBout.CovidVaccineNeed = CovidVaccineNeed;
            newBout.MaxWeightPerPessenger = MaxWeightPerPessenger;
            newBout.NumberOfSeats = NumberOfSeats;
            _context.Bouts.Add(newBout);
/*            _context.SaveChanges();*/
            return Task.FromResult(newBout);
        }


        [HttpGet("/Persons")]
        public Task<List<Person>> People()
        {
            List<Person> persons = _context.Persons.ToList();
            return Task.FromResult(persons);
        }
    }
}
