using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Controllers
{
	public class BookController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		

        public BookController(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

        public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Book book)
		{
			await _dbContext.Books.AddAsync(book);
			await _dbContext.SaveChangesAsync();
			
			return RedirectToAction("Index","Home");
		}

		
		public async Task<IActionResult> Delete(int id)
		{
			var book = await _dbContext.Books.FindAsync(id);

			if (book != null) {

				_dbContext.Books.Remove(book);
				await _dbContext.SaveChangesAsync();
			}

			return RedirectToAction("Index","Home");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var book = await _dbContext.Books.FindAsync(id);

			return View(book);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Book newBook)
		{
			var book = await _dbContext.Books.FindAsync(newBook.Id);

			if (book != null) {

				book.Title = newBook.Title;
				book.Author = newBook.Author;
				book.Language = newBook.Language;
				book.Borrowed = newBook.Borrowed;
			}

			await _dbContext.SaveChangesAsync();

			return RedirectToAction("Index","Home");
		}

		public async Task<IActionResult> Details(int id)
		{
			var book = await _dbContext.Books.FindAsync(id);
			
			return View(book);
		}
	}
}
