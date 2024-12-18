using Domain.Entities;

namespace Book_Management_System.Repositories
{
	public interface IBorrowRecordRepository
	{
		Task<BorrowRecord> GetById(int id);
		Task Insert(BorrowRecord borrowRecord);
		void Delete(BorrowRecord borrowRecord);
		Task Save();

		Task<IEnumerable<BorrowRecord>> GetActiveRecords();
		Task<IEnumerable<BorrowRecord>> GetOverdueRecords();
		Task<List<BorrowRecord>> GetMemberActiveRecords(Member member);
		Task<List<BorrowRecord>> GetAllMemberRecords(Member member);
	}
}
