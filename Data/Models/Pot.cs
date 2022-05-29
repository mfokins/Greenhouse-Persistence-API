using Data.Models.Measurements;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Pot
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public int MoistureSensorId { get; set; }
        public int MoistureThresholdId { get; set; }
        [Required]
        public SensorStatus MoistureSensorStatus { get; set; }
        [ForeignKey("MoistureThresholdId")]
        public Threshold MoistureThreshold { get; set; }
        [Required]
        public List<MoistureMeasurement> MoistureMeasurements { get; set; }
        
    }
}