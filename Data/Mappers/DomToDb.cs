﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappers
{
    public class DomToDb
    {
        public static Models.TemperatureMeasurement Convert(Core.Models.TemperatureMeasurement entity)
        {
            return new Models.TemperatureMeasurement()
            {
                Temperature = entity.Temperature,
                Time = entity.Time
            };
        }

        public static Models.LuminosityMeasurement Convert(Core.Models.LuminosityMeasurement entity)
        {
            return new Models.LuminosityMeasurement()
            {
                Lux = entity.Lux,
                IsLit = entity.IsLit,
                Time = entity.Time
            };
        }
    }
}