

namespace Core.Models
{
    public class Pot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Threshold moistureThreshold { get; set; }
        public string GreenHouseId { get; set; }
        public int MoistureSensorId { get; set; }


    }
}
