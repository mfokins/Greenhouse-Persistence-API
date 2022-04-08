using Api.Models;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Mappers
{
    public class DomToApi
    {
        public static TemperatureMeasurement Convert(TemperatureMesurment temperatureMesurment)
        {
            return new Api.Models.TemperatureMeasurement
            {
                Temperature = temperatureMesurment.Temperature,
                Time = ((DateTimeOffset)temperatureMesurment.Time).ToUnixTimeSeconds()
            };
        }
    }
}
