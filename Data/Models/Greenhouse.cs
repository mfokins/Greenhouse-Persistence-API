using Data.Models.Measurements;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Greenhouse
    {
        [Key]
        public string GreenHouseId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        [Required]
        public List<TemperatureMeasurement> TemperatureMesurments { get; set; }
        
        [Required]
        public List<DioxideCarbonMeasurement> DioxideCarbonMeasurements { get; set; }
        
        [Required]
        public List<HumidityMeasurement> HumidityMeasurements { get; set; }
        
        [Required]
        public List<Threshold> Thresholds { get; set; }
        [Required]
        public List<Pot> Pots { get; set; }
        [Required]
        public List<SensorStatus> SensorStatuses { get; set; }
    }
}