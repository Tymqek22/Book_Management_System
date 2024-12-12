using Domain.Entities;

namespace Book_Management_System.Repositories
{
	public interface IBookRepository
	{
		Task<Book> GetById(int id);
		Task<Book> GetByIdWithBorrowRecords(int id);
		Task<IEnumerable<Book>> GetAll();
		Task Insert(Book book);
		Task Update(Book book);
		void Delete(Book book);
		Task Save();
		Task<IEnumerable<Book>> GetAvailableBooks();
		Task<IEnumerable<Book>> GetUnavailableBooks();
		Task<IEnumerable<Genre>> GetGenres();
	}
}
