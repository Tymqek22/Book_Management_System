using Domain.Entities;

namespace Book_Management_System.Interfaces
{
	public interface IBorrowService
	{
		Task LendBook(BorrowRecord borrowRecord);
		Task ReturnBook(int id);
		bool IsBookAvailable(Book book);

		Task TrackOverdue();
		void CalculateFine(BorrowRecord borrowRecord);
	}
}
