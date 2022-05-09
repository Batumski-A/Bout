using Boat_2.Data;
using Boat_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boat_2.Controllers
{
    [Route("Person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        Person _person = new();
        private readonly AppDbContext _context;
        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Task<Person> AddBoat(string FullNam,int Age,double Weight,bool Vaccined)
        {
            _person.FullName = FullNam;
            _person.Age = Age;
            _person.Weight = Weight;
            _person.Vaccinated = Vaccined;
            _context.Persons.Add(_person);
            _context.SaveChanges();
            return Task.FromResult(_person);
        }

    }
}
