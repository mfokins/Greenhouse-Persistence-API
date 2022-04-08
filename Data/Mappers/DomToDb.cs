using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappers
{
    public class DomToDb
    {
        public static Models.TemperatureMesurment Convert(Core.Models.TemperatureMesurment entity)
        {
            return new Models.TemperatureMesurment()
            {
                GreenHouseId = entity.GreenHouseId,
                Temperature = entity.Temperature,
                Time = entity.Time
            };
        }
    }
}
