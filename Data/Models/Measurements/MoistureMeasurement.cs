using System.ComponentModel.DataAnnotations;

namespace Data.Models.Measurements
{
    public class MoistureMeasurement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Moisture { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}