
using Microsoft.EntityFrameworkCore;
using TestImages.Models;


namespace TestImages.Data
{
   public class TestImagesDbContext : DbContext
    {
        public TestImagesDbContext(DbContextOptions<TestImagesDbContext> options ) : base (options)
        {

        }

        public DbSet<Images> Image { get; set; }
       
    }
}
