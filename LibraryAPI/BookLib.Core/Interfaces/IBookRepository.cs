using BookLib.Core.Models;

namespace BookLib.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetByAuthorAsync(string author);
    }
}