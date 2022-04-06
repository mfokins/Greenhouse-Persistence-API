using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Greenhouse
{
    [Required]
    [Key,Column( Order=0)]
    public int GreenhouseId { get; set; }
    
    public User? UserId { get; set; }
    
}