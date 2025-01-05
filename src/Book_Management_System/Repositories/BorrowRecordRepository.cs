using Book_Management_System.Repositories.Interfaces;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Repositories
{
	public class BorrowRecordRepository : IBorrowRecordRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public BorrowRecordRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<BorrowRecord> GetById(int id)
		{
			return await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.FirstOrDefaultAsync(br => br.Id == id);
		}

		public async Task Insert(BorrowRecord borrowRecord)
		{
			await _dbContext.BorrowRecords.AddAsync(borrowRecord);
		}

		public void Delete(BorrowRecord borrowRecord)
		{
			if (borrowRecord.Book != null) {

				borrowRecord.Book.Quantity++;
				borrowRecord.ReturnDate = DateTime.Now.Date;
				borrowRecord.IsActive = false;

				if (borrowRecord.IsOverdue) {

					borrowRecord.IsOverdue = false;
				}
			}
		}

		public async Task Save()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<BorrowRecord>> GetActiveRecords()
		{
			return await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.Include(m => m.Member)
				.Where(br => br.IsActive && br.ReturnDate == null)
				.ToListAsync();
		}

		public async Task<IEnumerable<BorrowRecord>> GetOverdueRecords()
		{
			return await _dbContext.BorrowRecords
				.Where(br => br.EndDate < DateTime.Now && br.IsActive)
				.ToListAsync();
		}

		public async Task<List<BorrowRecord>> GetMemberActiveRecords(Member member)
		{
			return await _dbContext.BorrowRecords
					.Include(b => b.Book)
					.Where(br => br.MemberId == member.Id && br.IsActive)
					.ToListAsync();
		}

		public async Task<List<BorrowRecord>> GetAllMemberRecords(Member member)
		{
			return await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.Where(br => br.MemberId == member.Id && br.Book != null)
				.OrderByDescending(br => br.IsActive)
				.ThenByDescending(br => br.EndDate)
				.ToListAsync();
		}
	}
}
