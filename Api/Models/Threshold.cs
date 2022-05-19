using System.ComponentModel.DataAnnotations;

namespace Api.Models
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
        public ThresholdType Type { get; set; }

        public double LowerThreshold { get; set; }
        public double? HigherThreshold { get; set; }
    }
}