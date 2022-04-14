using Api.Models;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminosityMeasurement = Core.Models.LuminosityMeasurement;
using ApiLuminosityMeasurement = Api.Models.LuminosityMeasurement;
using TemperatureMeasurement = Core.Models.TemperatureMeasurement;

namespace Api.Mappers
{
    public class ApiToDom
    {
        public static TemperatureMeasurement Convert(Models.TemperatureMeasurement value)
        {
            return new TemperatureMeasurement
            {
                GreenHouseId = value.GreenHouseId,
                Temperature = value.Temperature,
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }

        public static LuminosityMeasurement Convert(ApiLuminosityMeasurement value)
        {
            return new LuminosityMeasurement()
            {
                GreenHouseId = value.GreenHouseId,
                Lux = value.Lux,
                IsLit = value.Lux >= 200, //if data is received from IoT here we can assign boolean value ourselves
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}