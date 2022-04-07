

using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public  class TemperatureMesurment
    {   
        [Required]
        public int Temperature { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        public string GreenHouseId { get; set; }
    }
}
