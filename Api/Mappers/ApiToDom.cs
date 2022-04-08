using Api.Models;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Mappers
{
    public class ApiToDom
    {
        public static TemperatureMesurment Convert(TemperatureMeasurement value)
        {
            return new TemperatureMesurment
            {
                GreenHouseId = value.GreenHouseId,
                Temperature = value.Temperature,
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
