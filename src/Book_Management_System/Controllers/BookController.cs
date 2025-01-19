using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Book_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Book_Management_System.Repositories.Interfaces;
using Book_Management_System.Services.Interfaces;
using Book_Management_System.Utilities;

namespace Book_Management_System.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookRepository _repository;
		private readonly IBookService _bookService;
		private readonly ISortingService _sortingService;

        public BookController(IBookRepository repository, IBookService bookService,ISortingService sortingService)
        {
			_repository = repository;
			_bookService = bookService;
			_sortingService = sortingService;
        }

		public async Task<IActionResult> Index(int pageNumber,string sortBy = "Title")
		{
			await _bookService.TrackAvailability();
			
			var books = await _repository.GetAll();

			Func<Book,object> sortFunction = sortBy switch
			{
				"Title" => book => book.Title,
				"Author" => book => book.Author
			};

			var sortedBooks = _sortingService.Sort(books,sortFunction);

			return View(PaginatedList<Book>.Create(sortedBooks,pageNumber,10,sortBy));
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

			if (await _bookService.Delete(id)) {

				return RedirectToAction("Index");
			}

			var error = new ErrorViewModel
			{
				RequestId = "Book cannot be deleted, it is currently borrowed."
			};

			return View("Error",error);
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
		public async Task PopulateGenres()
		{
			var genres = await _repository.GetGenres();

			ViewBag.Genres = new SelectList(genres,"Id","Name");
		}
	}
}
