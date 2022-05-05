﻿using System.ComponentModel.DataAnnotations;

namespace Bout_2.Models
{
    public class Bout
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; } = string.Empty;
        [Required,MaxLength(32)]
        public string? Status { get; set; }
        public int NumberOfSeats { get; set; }
        public double MaxWeightPerPessenger { get; set; }
        public bool CovidVaccineNeed { get; set; }
        [MaxLength(32)]
        public string Pessengers { get; set; } = string.Empty;
    }
}
