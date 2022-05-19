using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public enum ThresholdType
    {
        Temperature,
        Humidity,
        DioxideCarbon,
        Moisture,
    }
    public class Threshold
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public ThresholdType Type { get; set; }

        public double LowerThreshold { get; set; }
        public double? HigherThreshold { get; set; }
    }
}