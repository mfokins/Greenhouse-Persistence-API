using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("PotId")]
        public Pot PotId  { get; set; }
    }
}