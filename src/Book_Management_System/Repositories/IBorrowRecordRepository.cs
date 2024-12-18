using Domain.Entities;

namespace Book_Management_System.Repositories
{
	public interface IBorrowRecordRepository
	{
		Task<BorrowRecord> GetById(int id);
		Task Insert(BorrowRecord borrowRecord);
		Task Update(BorrowRecord borrowRecord);
		void Delete(BorrowRecord borrowRecord);
		Task Save();

		Task<IEnumerable<BorrowRecord>> GetActiveRecords();
		Task<IEnumerable<BorrowRecord>> GetOverdueRecords();
	}
}
