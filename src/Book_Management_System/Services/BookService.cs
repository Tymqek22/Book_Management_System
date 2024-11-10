using Domain.Services;
using Domain.Persistence;
using Domain.Entities;

namespace Book_Management_System.Services
{
	public class BookService : IBookService
	{
		private readonly ApplicationDbContext _dbContext;

        public BookService(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

        public void AddBook(Book book)
		{
			_dbContext.Books.Add(book);
			_dbContext.SaveChanges();
		}

		public void DeleteBook(int id)
		{
			var result = _dbContext.Books.FirstOrDefault(b => b.Id == id);

			if (result != null) {

				_dbContext.Books.Remove(result);
				_dbContext.SaveChanges();
			}

		}

		public void EditBook(int id, Book newBook)
		{
			var result = _dbContext.Books.FirstOrDefault(b => b.Id == id);

			if (result != null) {

				result = newBook;
				_dbContext.SaveChanges();
			}
		}

		public Book GetBookById(int id)
		{
			return _dbContext.Books.FirstOrDefault(b => b.Id == id);
		}
	}
}
