using System.ComponentModel.DataAnnotations;

namespace Boat_2.Models
{
    public class Boat
    {
        [Key]
        public int Id { get; set; }
        [StringLength(32)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(32)]
        public string Status { get; set; } = string.Empty;
        public int CrewSeats { get; set; }
        [Required, StringLength(16)]
        public string QualificationNeed { get; set; } = string.Empty;
        [MaxLength(32)]
        public string Crews { get; set; } = string.Empty;
        public DateTime StartingDateTime { get; set; } = DateTime.Now;
    }
}
