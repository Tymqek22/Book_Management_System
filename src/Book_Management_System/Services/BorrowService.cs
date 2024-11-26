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

        public async Task<bool> LendBook(BorrowRecord borrowRecord)
		{
			var book = await _dbContext.Books.FindAsync(borrowRecord.BookId);

			if (book != null) {

				if (this.IsBookAvailable(book)) {

					book.Quantity--;

					await _dbContext.BorrowRecords.AddAsync(borrowRecord);
					await _dbContext.SaveChangesAsync();
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}

			return true;
		}

		public async Task<bool> ReturnBook(int id)
		{
			var borrowRecord = await _dbContext.BorrowRecords.FindAsync(id);

			if (borrowRecord != null) {

				var book = await _dbContext.Books.FindAsync(borrowRecord.BookId);

				if (book != null) {

					book.Quantity++;
					borrowRecord.ReturnDate = DateTime.Now.Date;
					borrowRecord.IsActive = false;
					
					await _dbContext.SaveChangesAsync();
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}

			return true;
		}

		public bool IsBookAvailable(Book book)
		{
			if (book.IsAvailable && book.Quantity > 0) {
				return true;
			}
			return false;
		}

		public async Task TrackOverdue()
		{
			var overdueBooks = await _dbContext.BorrowRecords
				.Where(br => br.EndDate < DateTime.Now && br.IsActive)
				.ToListAsync();

			foreach (var borrowRecord in overdueBooks) {

				if (!borrowRecord.IsOverdue) {
					borrowRecord.IsOverdue = true;
				}
				
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
