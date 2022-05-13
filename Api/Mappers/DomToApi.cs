
using TemperatureMeasurement = Core.Models.TemperatureMeasurement;
using HumidityMeasurement = Core.Models.HumidityMeasurement;


namespace Api.Mappers
{
    public class DomToApi
    {
        public static Models.TemperatureMeasurement Convert(TemperatureMeasurement temperatureMeasurement)
        {
            return new Api.Models.TemperatureMeasurement
            {
                Temperature = temperatureMeasurement.Temperature,
                Time = ((DateTimeOffset) temperatureMeasurement.Time).ToUnixTimeSeconds()
            };
        }


        
        public static Models.HumidityMeasurement Convert(HumidityMeasurement humidityMeasurement)
        {
            return new Api.Models.HumidityMeasurement()
            {       
                Humidity = humidityMeasurement.Humidity,
                Time = ((DateTimeOffset) humidityMeasurement.Time).ToUnixTimeSeconds()
            };
        }

        public static Models.Pot Convert(Core.Models.Pot pot)
        {
            return new Models.Pot()
            {
                LowerMoistureThreshold = pot.moistureThreshold,
                Name = pot.Name,
                Id = pot.Id,
            };
        }
    }
}