using BookLib.Core.Interfaces;
using BookLib.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLib.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetByAuthorAsync(string author)
        {
            
            return await _dbSet
                .Where(b => b.Author == author)
                .ToListAsync();
        }
    }
}