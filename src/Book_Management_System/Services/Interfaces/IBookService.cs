using Domain.Entities;

namespace Book_Management_System.Services.Interfaces
{
	public interface IBookService
	{
		Task<bool> Delete(int bookId);
		bool HasActiveBorrowRecords(Book book);
		Task TrackAvailability();
	}
}
