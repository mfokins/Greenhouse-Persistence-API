using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class HumidityMeasurement
    {
        public double Humidity { get; set; }
        public long Time { get; set; }
        
    }
}