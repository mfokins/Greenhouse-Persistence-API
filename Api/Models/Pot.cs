

namespace Api.Models
{
    public class Pot
    {
        public double LatestMoisture { get; set; }
        public string Name { get; set; }
        public double LowerMoistureThreshold { get; set; }
        public int Id { get; set; }
        public int MoistureSensorId { get; set; }


    }
}
