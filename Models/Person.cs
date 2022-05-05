using System.ComponentModel.DataAnnotations;

namespace Bout_2.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
 
    }
}
