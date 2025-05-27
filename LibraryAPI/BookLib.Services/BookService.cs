// BookLib.Services/BookService.cs
using BookLib.Core.Interfaces;
using BookLib.Core.Models;
using LibraryAPI.BookLib.Core.Models;

namespace BookLib.Services
{
    public class BookService
    {
        private readonly IRepository<Book> _bookRepo;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IRepository<Book> bookRepo, IUnitOfWork unitOfWork)
        {
            _bookRepo = bookRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepo.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException("Book not found");
            return book;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            if (book.Year > DateTime.Now.Year)
                throw new ArgumentException("Year cannot be in the future");

            await _bookRepo.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return book;
        }
    }
}