using Book_Management_System.Repositories.Interfaces;
using Book_Management_System.Services.Interfaces;
using Domain.Entities;

namespace Book_Management_System.Services
{
	public class BorrowService : IBorrowService
	{
		private readonly IBookRepository _bookRepository;
		private readonly IBorrowRecordRepository _borrowRecordRepository;

        public BorrowService(IBookRepository bookRepository,IBorrowRecordRepository borrowRecordRepository)
        {
			_bookRepository = bookRepository;
			_borrowRecordRepository = borrowRecordRepository;
        }

        public async Task<bool> LendBook(BorrowRecord borrowRecord)
		{
			var book = await _bookRepository.GetById((int)borrowRecord.BookId);

			if (book != null) {

				if (this.IsBookAvailable(book)) {

					book.Quantity--;

					await _borrowRecordRepository.Insert(borrowRecord);
					await _borrowRecordRepository.Save();
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
			var borrowRecord = await _borrowRecordRepository.GetById(id);

			if (borrowRecord != null) {

				_borrowRecordRepository.Delete(borrowRecord);
				await _borrowRecordRepository.Save();
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

		public async Task<IEnumerable<BorrowRecord>> GetActiveBorrowRecords()
		{
			return await _borrowRecordRepository.GetActiveRecords();
		}

		public async Task TrackOverdue()
		{
			var overdueBooks = await _borrowRecordRepository.GetOverdueRecords();

			foreach (var borrowRecord in overdueBooks) {
				
				this.CalculateFine(borrowRecord);
			}

			await _borrowRecordRepository.Save();
		}

		public void CalculateFine(BorrowRecord borrowRecord)
		{
			TimeSpan timeSpan = DateTime.Now.Date - borrowRecord.EndDate;

			int daysOverdue = timeSpan.Days;

			borrowRecord.OverdueFine = daysOverdue * 0.2m;
		}
	}
}
