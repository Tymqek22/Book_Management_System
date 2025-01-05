using Book_Management_System.Repositories.Interfaces;
using Book_Management_System.Services.Interfaces;
using Domain.Entities;
using System.Runtime.CompilerServices;

namespace Book_Management_System.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;

		public BookService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<bool> Delete(int bookId)
		{
			var book = await _bookRepository.GetByIdWithBorrowRecords(bookId);

			if (book != null && !HasActiveBorrowRecords(book)) {

				foreach (var borrow in book.BorrowRecord) {

					borrow.BookId = null;
				}

				_bookRepository.Delete(book);
				await _bookRepository.Save();
			}
			else {
				return false;
			}

			return true;
		}

		public bool HasActiveBorrowRecords(Book book)
		{
			return book.BorrowRecord.Any(br => br.IsActive);
		}

		public async Task TrackAvailability()
		{
			var unavailableBooks = await _bookRepository.GetUnavailableBooks();

			foreach (var book in unavailableBooks) {

				book.IsAvailable = false;
			}

			var availableBooks = await _bookRepository.GetAvailableBooks();

			foreach (var book in availableBooks) {

				book.IsAvailable = true;
			}

			await _bookRepository.Save();
		}
	}
}
