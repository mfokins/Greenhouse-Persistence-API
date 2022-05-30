using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public enum SensorType
    {
        Co2,
        Temperature,
        Humidity,
        Moisture
    }
    public class SensorStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public SensorType Type { get; set; }
        [Required]

        public bool IsWorking { get; set; }
    }
}