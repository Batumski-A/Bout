using System.ComponentModel.DataAnnotations;

namespace Boat_2.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(64)]
        public string? FullName { get; set; }
        [MaxLength(3)]
        public int Age { get; set; }
        public double Weight { get; set; }
        public bool Vaccinated { get; set; }
        public int BoatId { get; set; } = 0;
    }
}
