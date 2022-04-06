using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class LuminosityMeasurement
{
    [Required]
    [Key,Column( Order=0)]
    public int LuminosityId { get; set; }
    
    public Greenhouse GreenhouseId { get; set; }
    
    public decimal Luminosity { get; set; }
    
    public long Time { get; set; }
}