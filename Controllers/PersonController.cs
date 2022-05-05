using Bout_2.Data;
using Bout_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bout_2.Controllers
{
    [Route("Api/")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PersonController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public Task<List<Person>> People()
        {
            List<Person> persons = _context.person.ToList();
            return Task.FromResult(persons);
        }
    }
}
