using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Book_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Management_System.Controllers
{
	public class BookController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

        public BookController(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

		public async Task<IActionResult> Index()
		{
			await this.TrackAvailibility();
			var books = await _dbContext.Books
				.Include(b => b.Genre)
				.ToListAsync();

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

				await _dbContext.Books.AddAsync(book);
				await _dbContext.SaveChangesAsync();
			}
			else {
				return View(book);
			}
			
			
			return RedirectToAction("Index");
		}

		
		public async Task<IActionResult> Delete(int id)
		{
			var book = await _dbContext.Books.FindAsync(id);

			book.BorrowRecord = await _dbContext.BorrowRecords
				.Where(br => br.BookId == book.Id)
				.ToListAsync();


			if (book != null) {

				if (!book.BorrowRecord.Any(br => br.IsActive)) {

					foreach (var borrow in book.BorrowRecord) {

						borrow.BookId = null;
					}

					_dbContext.Books.Remove(book);
					await _dbContext.SaveChangesAsync();
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
			var book = await _dbContext.Books
				.Include(b => b.Genre)
				.FirstOrDefaultAsync(b => b.Id == id);

			await PopulateGenres();

			return View(book);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Book newBook)
		{
			if (ModelState.IsValid) {

				var book = await _dbContext.Books.FindAsync(newBook.Id);

				if (book != null) {

					book.Title = newBook.Title;
					book.Author = newBook.Author;
					book.GenreId = newBook.GenreId;
					book.Language = newBook.Language;
					book.IsAvailable = newBook.IsAvailable;
					book.Quantity = newBook.Quantity;
				}

				await _dbContext.SaveChangesAsync();
			}
			else {
				return View(newBook);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Details(int id)
		{
			var book = await _dbContext.Books
				.Include(b => b.Genre)
				.FirstOrDefaultAsync(b => b.Id == id);
			
			return View(book);
		}

		[NonAction]
		public async Task TrackAvailibility()
		{
			var unavailableBooks = await _dbContext.Books
				.Where(b => b.Quantity == 0 && b.IsAvailable)
				.ToListAsync();

            foreach (var book in unavailableBooks) {

                book.IsAvailable = false;
            }

			var availableBooks = await _dbContext.Books
				.Where(b => b.Quantity > 0 && !b.IsAvailable)
				.ToListAsync();

            foreach (var book in availableBooks) {

                book.IsAvailable = true;
            }

            await _dbContext.SaveChangesAsync();
		}

		[NonAction]
		public async Task PopulateGenres()
		{
			var genres = await _dbContext.Genres.ToListAsync();

			ViewBag.Genres = new SelectList(genres,"Id","Name");
		}
	}
}
