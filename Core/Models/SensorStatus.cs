
namespace Core.Models
{
    public enum SensorType
    {
        Co2,
        Temperature,
        Humidity,
        Moisture
    }
    public class SensorStatus
    {
        public SensorType Type { get; set; }
        public bool IsWorking { get; set; }
    }
}
