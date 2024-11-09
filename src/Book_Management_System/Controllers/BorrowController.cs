using Domain.Entities;
using Domain.DTO;
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Management_System.Controllers
{
	public class BorrowController : Controller
	{
		private readonly TempDB _context;

        public BorrowController(TempDB context)
        {
			_context = context;
        }

        public IActionResult Index()
		{
			var records = _context.BorrowRecords;

			return View(records);
		}

		public IActionResult Create(int id)
		{
			var book = _context.Books.FirstOrDefault(b => b.Id == id);
			ViewBag.Book = book;
			PopulateMembers();

			return View();
		}

		[HttpPost]
		public IActionResult Create(BorrowRecord model)
		{
			model.Member = _context.Members.FirstOrDefault(m => m.Id == model.MemberId);
			model.Book = _context.Books.FirstOrDefault(b => b.Id == model.BookId);

			_context.BorrowRecords.Add(model);
			return RedirectToAction("Index");
		}

		[NonAction]
		private void PopulateMembers()
		{
			var members = _context.Members
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
