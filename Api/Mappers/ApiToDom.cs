using Api.Models;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TemperatureMeasurement = Core.Models.TemperatureMeasurement;
using HumidityMeasurement = Core.Models.HumidityMeasurement;
using Pot = Core.Models.Pot;

namespace Api.Mappers
{
    public class ApiToDom
    {
        public static TemperatureMeasurement Convert(Models.TemperatureMeasurement value)
        {
            return new TemperatureMeasurement
            {
                Temperature = value.Temperature,
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }



        public static HumidityMeasurement Convert(Models.HumidityMeasurement value)
        {
            return new HumidityMeasurement()
            {
                Humidity = value.Humidity,
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }

        public static Pot Convert(Models.Pot pot)
        {
            throw new NotImplementedException();
            return new Pot()
            {
                moistureThreshold = new Core.Models.Threshold()
                {
                    Type = ThresholdType.Moisture,
                    LowerThreshold = pot.LowerMoistureThreshold
                },
                Name = pot.Name,
                Id = pot.Id,
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