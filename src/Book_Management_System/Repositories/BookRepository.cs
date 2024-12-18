using Book_Management_System.Repositories.Interfaces;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public BookRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Book> GetById(int id)
		{
			var book = await _dbContext.Books
				.Include(b => b.Genre)
				.FirstOrDefaultAsync(b => b.Id == id);

			return book;
		}

		public async Task<Book> GetByIdWithBorrowRecords(int id)
		{
			var book = await _dbContext.Books
				.Include(b => b.Genre)
				.FirstOrDefaultAsync(b => b.Id == id);

			if (book != null) {

				book.BorrowRecord = await _dbContext.BorrowRecords
				.Where(br => br.BookId == id)
				.ToListAsync();
			}

			return book;
		}

		public async Task<IEnumerable<Book>> GetAll()
		{
			var books = await _dbContext.Books
				.Include(b => b.Genre)
				.ToListAsync();

			return books;
		}

		public async Task Insert(Book book)
		{
			await _dbContext.Books.AddAsync(book);
		}

		public async Task Update(Book book)
		{
			var bookInDb = await this.GetById(book.Id);

			if (bookInDb != null) {

				bookInDb.Title = book.Title;
				bookInDb.Author = book.Author;
				bookInDb.GenreId = book.GenreId;
				bookInDb.Language = book.Language;
				bookInDb.IsAvailable = book.IsAvailable;
				bookInDb.Quantity = book.Quantity;
			}
		}

		public void Delete(Book book)
		{
			_dbContext.Books.Remove(book);
		}

		public async Task Save()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Book>> GetAvailableBooks()
		{
			var availableBooks = await _dbContext.Books
				.Where(b => b.Quantity > 0)
				.ToListAsync();

			return availableBooks;
		}

		public async Task<IEnumerable<Book>> GetUnavailableBooks()
		{
			var unavailableBooks = await _dbContext.Books
				.Where(b => b.Quantity == 0)
				.ToListAsync();

			return unavailableBooks;
		}

		public async Task<IEnumerable<Genre>> GetGenres()
		{
			var genres = await _dbContext.Genres.ToListAsync();

			return genres;
		}
	}
}
