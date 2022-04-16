using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class HumidityMeasurement
    {
        public string GreenHouseId { get; set; }
        public int Humidity { get; set; }
        public DateTime Time { get; set; }
        
    }
}