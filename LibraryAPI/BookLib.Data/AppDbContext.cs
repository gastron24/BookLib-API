using Microsoft.EntityFrameworkCore;
using BookLib.Core.Models;


namespace BookLib.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}