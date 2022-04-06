using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class HumidityMeasurement
{
   
    [Required]
    [Key,Column( Order=0)]
    public int HumidityId { get; set; }
    public Greenhouse GreenhouseId { get; set; }
    
    public decimal Humidity { get; set; }
    
    public long Time { get; set; }
}