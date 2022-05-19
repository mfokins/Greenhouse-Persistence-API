using Data.Models.Measurements;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Greenhouse
    {
        [Key]
        public string GreenHouseId { get; set; }
        [Required]
        public IList<TemperatureMeasurement> TemperatureMesurments { get; set; }
        
        [Required]
        public IList<DioxideCarbonMeasurement> DioxideCarbonMeasurements { get; set; }
        
        [Required]
        public IList<HumidityMeasurement> HumidityMeasurements { get; set; }
        
        [Required]
        public IList<Threshold> Thresholds { get; set; }
        [Required]
        public IList<Pot> Pots { get; set; }

    }
}
