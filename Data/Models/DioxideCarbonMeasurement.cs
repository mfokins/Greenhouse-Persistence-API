using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class DioxideCarbonMeasurement
    {
        [Required]
        [Key,Column( Order=0)]
        public int Id { get; set; }
        [Required]
        public double Co2Measurement { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}