using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Services;

namespace Book_Management_System.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
			_bookService = bookService;
        }

        public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Book book)
		{
			_bookService.AddBook(book);
			return RedirectToAction("Index","Home");
		}

		public IActionResult Delete(int id)
		{
			_bookService.DeleteBook(id);
			return RedirectToAction("Index","Home");
		}

		public IActionResult Edit(int id)
		{
			var result = _bookService.GetBookById(id);

			return View(result);
		}

		[HttpPost]
		public IActionResult Edit(Book book)
		{
			_bookService.EditBook(book.Id,book);
			return RedirectToAction("Index","Home");
		}

		public IActionResult Details(int id)
		{
			var result = _bookService.GetBookById(id);
			
			return View(result);
		}
	}
}
