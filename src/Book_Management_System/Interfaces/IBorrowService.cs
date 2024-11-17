using Domain.Entities;

namespace Book_Management_System.Interfaces
{
	public interface IBorrowService
	{
		Task<bool> LendBook(BorrowRecord borrowRecord);
		Task<bool> ReturnBook(int id);

		Task TrackOverdue();
		void CalculateFine(BorrowRecord borrowRecord);
	}
}
