using System;

namespace Core.Models
{
    public class DioxideCarbonMeasurement
    {
        public double Co2Measurement { get; set; }
        public DateTime Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}