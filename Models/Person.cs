using System.ComponentModel.DataAnnotations;

namespace Boat_2.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(64)]
        public string FullName { get; set; } = string.Empty;
        [MaxLength(3)]
        public int Age { get; set; }
        [Required,StringLength(16)]
        public string Qualification { get; set; } = string.Empty;
        public int BoatId { get; set; } = 0;
        public byte[] Picture { get; set; } = new byte[64];

    }
}
