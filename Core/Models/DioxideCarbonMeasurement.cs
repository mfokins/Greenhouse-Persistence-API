using System;

namespace Core.Models
{
    public class DioxideCarbonMeasurement
    {
        public int Co2Measurement { get; set; }
        public DateTime Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}