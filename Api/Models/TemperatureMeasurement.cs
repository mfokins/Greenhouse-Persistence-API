namespace Api.Models
{
    public class TemperatureMeasurement
    {
        public int Temperature { get; set; }
        public long Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}