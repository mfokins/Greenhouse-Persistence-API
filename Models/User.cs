using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    [Required]
    [Key,EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    //Can be relevant to add the id/token from the arduino to know where from which arduino 
    //@author Nick
}