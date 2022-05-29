
namespace Core.Models
{
    public enum SensorType
    {
        Co2,
        Temperature,
        Humidity,
        Moisture,
        Empty
    }
    public class SensorStatus
    {
        public SensorType Type { get; set; }
        public bool IsWorking { get; set; }
        public static SensorStatus Empty
        {
            get
            {
                return new SensorStatus
                {
                    Type = SensorType.Empty
                };
            }
        }
    }
}
