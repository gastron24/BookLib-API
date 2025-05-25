using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
namespace LibraryAPI.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        
        
        
       
    }
}
