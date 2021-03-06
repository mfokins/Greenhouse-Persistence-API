using Data.Models;
using Data.Models.Measurements;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class GreenHouseDbContext : DbContext
    {
        public DbSet<Greenhouse> Greenhouses { get; set; }
        public GreenHouseDbContext(DbContextOptions<GreenHouseDbContext> options) : base(options)
        {

        }
        public GreenHouseDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=database-2.citlownhihib.eu-central-1.rds.amazonaws.com,1433;Database=GreenHouse;User Id=admin; Password=12345678;");
            //#if DEBUG
            //            optionsBuilder.UseSqlServer("Server= localhost; Database= Greenhouse; Integrated Security=True;");

            //#else

            //                        optionsBuilder.UseSqlServer("Server=database-2.citlownhihib.eu-central-1.rds.amazonaws.com,1433;Database=GreenHouse;User Id=admin; Password=12345678;");
            //#endif
        }


    }
}
