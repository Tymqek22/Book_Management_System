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
			var records = await _dbContext.BorrowRecords.ToListAsync();

			return View(records);
		}

		public async Task<IActionResult> Create(int id)
		{
			var book = await _dbContext.Books.FindAsync(id);
			ViewBag.Book = book;
			PopulateMembers();

			return View();
		}

		//TODO: fixing sql joins
		[HttpPost]
		public async Task<IActionResult> Create(BorrowRecord model)
		{
			model.Member = _dbContext.Members.FirstOrDefault(m => m.Id == model.MemberId);
			model.Book = _dbContext.Books.FirstOrDefault(b => b.Id == model.BookId);

			await _dbContext.BorrowRecords.AddAsync(model);
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
