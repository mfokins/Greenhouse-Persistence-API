using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappers
{
    public class DbToDom
    {
        public static Core.Models.TemperatureMeasurement Convert(Models.TemperatureMeasurement temperatureMeasurement)
        {
            return new Core.Models.TemperatureMeasurement()
            {
                Temperature = temperatureMeasurement.Temperature,
                Time = temperatureMeasurement.Time,
            };
        }

        public static Core.Models.LuminosityMeasurement Convert(Models.LuminosityMeasurement luminosityMeasurement)
        {
            return new Core.Models.LuminosityMeasurement()
            {
                Lux = luminosityMeasurement.Lux,
                IsLit = luminosityMeasurement.IsLit,
                Time = luminosityMeasurement.Time
            };
        }
        public static Core.Models.DioxideCarbonMeasurement Convert(Models.DioxideCarbonMeasurement dioxideCarbonMeasurement)
        {
            return new Core.Models.DioxideCarbonMeasurement()
            {
                Co2Measurement = dioxideCarbonMeasurement.Co2Measurement,
                Time = dioxideCarbonMeasurement.Time
            };
        }

        public static Core.Models.HumidityMeasurement Convert(Models.HumidityMeasurement humidityMeasurement)
        {
            return new Core.Models.HumidityMeasurement()
            {
                Humidity = humidityMeasurement.Humidity,
                Time = humidityMeasurement.Time
            };
        }
    }
}