namespace Api.Models
{
    public class LuminosityMeasurement
    {
        public int Lux { get; set; }
        public bool IsLit { get; set; }
        public long Time { get; set; }
        public string GreenHouseId { get; set; }
    }
}