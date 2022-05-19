using Data.Models.Measurements;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Pot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public string Name { get; set; }
        [Required]
        
        public Threshold MoistureThreshold { get; set; }
        [Required]
        public IList<MoistureMeasurement> MoistureMeasurements { get; set; }
    }
}