using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappers
{
    public class DbToDom
    {
        public static Core.Models.TemperatureMesurment Convert(Models.TemperatureMesurment temperatureMesurment)
        {
            return new Core.Models.TemperatureMesurment()
            {
                Temperature = temperatureMesurment.Temperature,
                Time = temperatureMesurment.Time,
            };
        }
    }
}
