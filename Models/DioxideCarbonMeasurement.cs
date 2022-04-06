using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class DioxideCarbonMeasurement
{
    [Required]
    [Key,Column( Order=0)]
    public int DioxideCarbonId { get; set; }
    
    public Greenhouse GreenhouseId { get; set; }
    
    public decimal DioxideCarbon { get; set; }
    
    public long Time { get; set; }
    //A FK to the GreenhouseId
}