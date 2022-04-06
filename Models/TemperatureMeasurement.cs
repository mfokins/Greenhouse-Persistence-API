using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public class TemperatureMeasurement
    {
        
        [Required]
        [Key,Column( Order=0)]
        public int TemperatureId { get; set; }
        
        public Greenhouse GreenhouseId { get; set; }
        // I would change the temp to be a decimal
        //@author Nick
        public int Temperature { get; set; }
        public long Time { get; set; }
        
    }

