using Library.Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Backend.Data
{
    public class LibraryDbContext:DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options ):base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
