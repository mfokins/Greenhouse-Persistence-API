namespace Core.Models
{
    public enum ThresholdType
    {
        Temperature,
        Humidity,
        DioxideCarbon,
        Moisture,
        Empty
    }
    public class Threshold
    {
        public ThresholdType Type { get; set; }
        public double LowerThreshold { get; set; }
        public double? HigherThreshold { get; set; }
        public static Threshold Empty
        {
            get
            {
                return new Threshold
                {
                    Type = ThresholdType.Empty
                };
            }
        }
    }
}