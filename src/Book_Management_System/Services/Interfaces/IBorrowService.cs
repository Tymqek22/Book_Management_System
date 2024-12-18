using Domain.Entities;

namespace Book_Management_System.Services.Interfaces
{
	public interface IBorrowService
	{
		Task<bool> LendBook(BorrowRecord borrowRecord);
		Task<bool> ReturnBook(int id);
		bool IsBookAvailable(Book book);

		Task<IEnumerable<BorrowRecord>> GetActiveBorrowRecords();
		Task TrackOverdue();
		void CalculateFine(BorrowRecord borrowRecord);
	}
}
