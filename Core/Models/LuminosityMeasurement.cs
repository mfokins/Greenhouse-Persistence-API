using System;

namespace Core.Models
{
    public class LuminosityMeasurement
    {
        public int Lux { get; set; }
        public bool IsLit { get; set; }
        public DateTime Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}