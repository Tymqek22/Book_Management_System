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
			var record = await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.FirstOrDefaultAsync(br => br.Id == id);

			return record;
		}

		public async Task Insert(BorrowRecord borrowRecord)
		{
			await _dbContext.BorrowRecords.AddAsync(borrowRecord);
		}

		public Task Update(BorrowRecord borrowRecord)
		{
			throw new NotImplementedException();
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
			var activeRecords = await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.Include(m => m.Member)
				.Where(br => br.IsActive && br.ReturnDate == null)
				.ToListAsync();

			return activeRecords;
		}

		public async Task<IEnumerable<BorrowRecord>> GetOverdueRecords()
		{
			var overdueBooks = await _dbContext.BorrowRecords
				.Where(br => br.EndDate < DateTime.Now && br.IsActive)
				.ToListAsync();

			return overdueBooks;
		}
	}
}
