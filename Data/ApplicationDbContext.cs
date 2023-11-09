using DockerComposeSample.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerComposeSample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
