using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Book_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Book_Management_System.Repositories.Interfaces;

namespace Book_Management_System.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
			_repository = repository;
        }

		public async Task<IActionResult> Index()
		{
			await this.TrackAvailibility();
			
			var books = await _repository.GetAll();

			return View(books);
		}

		public async Task<IActionResult> Add()
		{
			await PopulateGenres();

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Book book)
		{
			if (ModelState.IsValid) {

				await _repository.Insert(book);
				await _repository.Save();
			}
			else {
				return View(book);
			}
			
			
			return RedirectToAction("Index");
		}

		
		public async Task<IActionResult> Delete(int id)
		{
			var book = await _repository.GetByIdWithBorrowRecords(id);

			if (book != null) {

				if (!book.BorrowRecord.Any(br => br.IsActive)) {

					foreach (var borrow in book.BorrowRecord) {

						borrow.BookId = null;
					}

					_repository.Delete(book);
					await _repository.Save();
				}
				else {

					var error = new ErrorViewModel
					{
						RequestId = "Book cannot be deleted, it is currently borrowed."
					};

					return View("Error",error);
				}
				
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(int id)
		{

			var book = await _repository.GetById(id);

			await PopulateGenres();

			return View(book);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Book newBook)
		{
			if (ModelState.IsValid) {

				await _repository.Update(newBook);
				await _repository.Save();
			}
			else {
				return View(newBook);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Details(int id)
		{

			var book = await _repository.GetById(id);
			
			return View(book);
		}

		[NonAction]
		public async Task TrackAvailibility()
		{
			var unavailableBooks = await _repository.GetUnavailableBooks();

            foreach (var book in unavailableBooks) {

                book.IsAvailable = false;
            }

			var availableBooks = await _repository.GetAvailableBooks();

            foreach (var book in availableBooks) {

                book.IsAvailable = true;
            }

			await _repository.Save();
		}

		[NonAction]
		public async Task PopulateGenres()
		{
			var genres = await _repository.GetGenres();

			ViewBag.Genres = new SelectList(genres,"Id","Name");
		}
	}
}
