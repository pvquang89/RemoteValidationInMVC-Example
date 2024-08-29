using Microsoft.EntityFrameworkCore;

namespace MyMVC06_RemoteValidation.Models
{
    public class EFCoreDBContext : DbContext
    {

        
        public EFCoreDBContext(DbContextOptions<EFCoreDBContext> options) : base(options) 
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        //entity
        public DbSet<User> Users { get; set; }  
    }
}
