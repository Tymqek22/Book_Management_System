using Domain.Entities;
using Domain.DTO;
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book_Management_System.Interfaces;
using Book_Management_System.Models;

namespace Book_Management_System.Controllers
{
	public class BorrowController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IBorrowService _borrowService;

        public BorrowController(ApplicationDbContext dbContext, IBorrowService borrowService)
        {
			_dbContext = dbContext;
			_borrowService = borrowService;
        }

        public async Task<IActionResult> Index()
		{
			await _borrowService.TrackOverdue();

			var records = await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.Include(m => m.Member)
				.Where(br => br.IsActive)
				.ToListAsync();

			return View(records);
		}

		public async Task<IActionResult> Create(int bookId)
		{
			var book = await _dbContext.Books.FindAsync(bookId);
			ViewBag.Book = book;
			PopulateMembers();

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
		private void PopulateMembers()
		{
			var members = _dbContext.Members
				.Select(m => new MemberDto
				{
					Id = m.Id,
					FullName = m.FirstName + " " + m.LastName
				})
				.OrderBy(d => d.FullName)
				.ToList();

			ViewBag.Members = new SelectList(members,"Id","FullName");
		}
	}
}
