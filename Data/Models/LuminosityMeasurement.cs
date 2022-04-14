using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class LuminosityMeasurement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Lux { get; set; }  // if we will need the certain amount in the future
        [Required]
        public bool IsLit { get; set; }  //to show if greenhouse is currently in daylight
        [Required]
        public DateTime Time { get; set; }
    }
}