using System.ComponentModel.DataAnnotations;

namespace Data.Models.Measurements
{
    public class HumidityMeasurement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Humidity { get; set; }
        [Required]
        public DateTime Time { get; set; }
        
    }
}