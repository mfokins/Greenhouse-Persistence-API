using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class GreenHouseDbContext : DbContext
    {
       public DbSet<TemperatureMesurment> TemperatureMesurments { get; set; }

        public GreenHouseDbContext(DbContextOptions<GreenHouseDbContext> options) : base(options)
        {
            
        }



    }
}