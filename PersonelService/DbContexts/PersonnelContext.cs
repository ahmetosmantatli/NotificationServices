using Microsoft.EntityFrameworkCore;
using PersonelService.Models;

namespace PersonelService.DbContexts
{
    public class PersonnelContext : DbContext
    {
        public PersonnelContext(DbContextOptions<PersonnelContext> options) : base(options)
        {
        }

        public DbSet<Personnel> Personnels { get; set; } // Personnel tablosunu temsil eder

       
    }
}
