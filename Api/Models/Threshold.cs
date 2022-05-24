using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Threshold
    {
        public double LowerThreshold { get; set; }
        public double? HigherThreshold { get; set; }
    }
}