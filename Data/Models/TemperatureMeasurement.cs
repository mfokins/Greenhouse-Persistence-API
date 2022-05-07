

using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public  class TemperatureMeasurement
    {   
        [Required]
        public float Temperature { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Key]
        public int Id { get; set; }
    }
}
