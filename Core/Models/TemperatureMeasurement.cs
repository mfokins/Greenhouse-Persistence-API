

namespace Core.Models
{
    public class TemperatureMeasurement
    {
        public float Temperature { get; set; }
        public DateTime Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}