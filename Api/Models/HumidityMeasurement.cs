using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class HumidityMeasurement
    {
        public string GreenHouseId { get; set; }
        public double Humidity { get; set; }
        public long Time { get; set; }
        
    }
}