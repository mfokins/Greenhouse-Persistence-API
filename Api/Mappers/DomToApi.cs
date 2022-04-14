using Api.Models;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminosityMeasurement = Api.Models.LuminosityMeasurement;
using CoreLuminosityMeasurement = Core.Models.LuminosityMeasurement;
using TemperatureMeasurement = Core.Models.TemperatureMeasurement;


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

        public static LuminosityMeasurement Convert(CoreLuminosityMeasurement luminosityMeasurement)
        {
            return new LuminosityMeasurement()
            {
                GreenHouseId = luminosityMeasurement.GreenHouseId,
                Lux = luminosityMeasurement.Lux,
                IsLit = luminosityMeasurement.Lux >=
                        200, //if data is received from IoT here we can assign boolean value ourselves
                Time = ((DateTimeOffset) luminosityMeasurement.Time).ToUnixTimeSeconds()
            };
        }
    }
}