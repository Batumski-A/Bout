using Boat_2.Data;
using Boat_2.Helpers;
using Boat_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boat_2.Controllers
{
    [Route("Api/Person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        Person _person = new();
        private readonly AppDbContext _context;
        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Person>> People()
        {
            List<Person> persons = _context.Persons.ToList();
            return Task.FromResult(persons);
        }


        [HttpPost]
        public Task<Person> AddPerson(string FullNam,int Age,List<PersonQualification> Qualification)
        {
            _person.FullName = FullNam;
            _person.Age = Age;
            string combinedString = string.Join(",", Qualification);
            _person.Qualification = combinedString;
            _context.Persons.Add(_person);
            _context.SaveChanges();
            return Task.FromResult(_person);
        }

    }
}
