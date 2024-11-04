using Domain.Services;
using Domain.Persistence;
using Domain.Entities;

namespace Book_Management_System.Services
{
	public class BookService : IBookService
	{
		private readonly TempDB _context;

        public BookService(TempDB context)
        {
			_context = context;
        }

        public void AddBook(Book book)
		{
			_context.Books.Add(book);
		}

		public void DeleteBook(int id)
		{
			var result = _context.Books.FirstOrDefault(b => b.Id == id);

			if (result != null) {

				_context.Books.Remove(result);
			}
		}

		public void EditBook(int id, Book newBook)
		{
			var result = _context.Books.FirstOrDefault(b => b.Id == id);

			if (result != null) {

				int index = _context.Books.IndexOf(result);
				_context.Books[index] = newBook;
			}
		}

		public Book GetBookById(int id)
		{
			return _context.Books.FirstOrDefault(b => b.Id == id);
		}
	}
}
