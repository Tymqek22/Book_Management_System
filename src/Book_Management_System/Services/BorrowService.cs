using Book_Management_System.Interfaces;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Services
{
	public class BorrowService : IBorrowService
	{
		private readonly ApplicationDbContext _dbContext;

        public BorrowService(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

        public async Task LendBook(BorrowRecord borrowRecord)
		{
			var book = await _dbContext.Books.FindAsync(borrowRecord.BookId);

			if (IsBookAvailable(book)) {

				book.Borrowed = true;

				await _dbContext.BorrowRecords.AddAsync(borrowRecord);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task ReturnBook(int id)
		{
			var borrowRecord = await _dbContext.BorrowRecords.FindAsync(id);

			if (borrowRecord != null) {

				var book = await _dbContext.Books.FindAsync(borrowRecord.BookId);

				book.Borrowed = false;

				_dbContext.BorrowRecords.Remove(borrowRecord);
			}

			await _dbContext.SaveChangesAsync();
		}

		public bool IsBookAvailable(Book book)
		{
			return !book.Borrowed;
		}

		public async Task TrackOverdue()
		{
			var overdueBooks = await _dbContext.BorrowRecords
				.Where(br => br.EndDate < DateTime.Now)
				.ToListAsync();

			foreach (var borrowRecord in overdueBooks) {

				borrowRecord.IsOverdue = true;
				this.CalculateFine(borrowRecord);
			}

			await _dbContext.SaveChangesAsync();
		}

		public void CalculateFine(BorrowRecord borrowRecord)
		{
			TimeSpan timeSpan = DateTime.Now.Date - borrowRecord.EndDate;

			int daysOverdue = timeSpan.Days;

			borrowRecord.OverdueFine = daysOverdue * 0.2m;
		}
	}
}
