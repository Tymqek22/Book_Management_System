using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Book_Management_System.ViewModels;
using Book_Management_System.Services.Interfaces;
using Book_Management_System.Repositories.Interfaces;

namespace Book_Management_System.Controllers
{
	public class BorrowController : Controller
	{
		private readonly IBorrowService _borrowService;
		private readonly IBookRepository _bookRepository;
		private readonly IMemberRepository _memberRepository;

        public BorrowController(IBorrowService borrowService,IBookRepository bookRepository,IMemberRepository memberRepository)
        {
			_borrowService = borrowService;
			_bookRepository = bookRepository;
			_memberRepository = memberRepository;
        }

        public async Task<IActionResult> Index()
		{
			await _borrowService.TrackOverdue();

			var records = await _borrowService.GetActiveBorrowRecords();

			return View(records);
		}

		public async Task<IActionResult> Create(int bookId)
		{
			var book = await _bookRepository.GetById(bookId);
			ViewBag.Book = book;
			await PopulateMembers();

			return View();
		}

		
		[HttpPost]
		public async Task<IActionResult> Create(BorrowRecord model)
		{
			if (!(await _borrowService.LendBook(model))) {

				var error = new ErrorViewModel()
				{
					RequestId = "Book cannot be issued."
				};

				return View("Error",error);
			}

			return RedirectToAction("Index");
		}

		
		public async Task<IActionResult> Return(int id)
		{
			if (!(await _borrowService.ReturnBook(id))) {

				var error = new ErrorViewModel()
				{
					RequestId = "Book cannot be returned."
				};

				return View("Error",error);
			}

			return RedirectToAction("Index");
		}

		[NonAction]
		private async Task PopulateMembers()
		{
			var members = await _memberRepository.PopulateMembers();

			ViewBag.Members = new SelectList(members,"Id","FullName");
		}
	}
}
