namespace Api.Models
{
    public class TemperatureMeasurement
    {
        public float Temperature { get; set; }
        public long Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}