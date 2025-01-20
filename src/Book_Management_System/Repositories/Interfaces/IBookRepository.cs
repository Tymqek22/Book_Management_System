using Book_Management_System.DTO;
using Domain.Entities;

namespace Book_Management_System.Repositories.Interfaces
{
	public interface IBookRepository
	{
		Task<Book> GetById(int id);
		Task<Book> GetByIdWithBorrowRecords(int id);
		Task<IEnumerable<Book>> GetAll();
		Task<IEnumerable<BookBorrowedDto>> GetAllWithStats();
		Task Insert(Book book);
		Task Update(Book book);
		void Delete(Book book);
		Task Save();
		Task<IEnumerable<Book>> GetAvailableBooks();
		Task<IEnumerable<Book>> GetUnavailableBooks();
		Task<IEnumerable<Genre>> GetGenres();
	}
}
