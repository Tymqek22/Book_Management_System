using Domain.Entities;
using Domain.DTO;
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Controllers
{
	public class BorrowController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

        public BorrowController(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
		{
			var records = await _dbContext.BorrowRecords
				.Include(m => m.Member)
				.Include(b => b.Book)
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
			var book = await _dbContext.Books.FindAsync(model.BookId);

			if (book != null) {

				book.Borrowed = true;
			}

			await _dbContext.BorrowRecords.AddAsync(model);
			await _dbContext.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		
		public async Task<IActionResult> Return(int id)
		{
			var borrowRecord = await _dbContext.BorrowRecords.FindAsync(id);

			if (borrowRecord != null) {

				var book = await _dbContext.Books.FindAsync(borrowRecord.BookId);

				book.Borrowed = false;
				
				_dbContext.BorrowRecords.Remove(borrowRecord);
			}

			await _dbContext.SaveChangesAsync();

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
