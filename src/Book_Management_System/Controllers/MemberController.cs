using Domain.Entities;
using Domain.Entities.ViewModels;
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Controllers
{
	public class MemberController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

        public MemberController(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

		public async Task<IActionResult> Index()
		{
			var members = await _dbContext.Members.ToListAsync();

			return View(members);
		}

		public async Task<IActionResult> Details(int id)
		{
			var member = await _dbContext.Members.FindAsync(id);

			var borrowRecords = await _dbContext.BorrowRecords
					.Include(b => b.Book)
					.Where(br => br.MemberId == member.Id)
					.ToListAsync();


			var memberViewModel = new MemberViewModel
			{
				Member = member,
				BorrowRecords = borrowRecords,
			};

			return View(memberViewModel);
		}

        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(Member newMember)
		{
			if (ModelState.IsValid) {

				await _dbContext.Members.AddAsync(newMember);
				await _dbContext.SaveChangesAsync();
			}
			else {
				return View(newMember);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			var member = await _dbContext.Members.FindAsync(id);

			if (member != null) {

				_dbContext.Members.Remove(member);
				await _dbContext.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Update(int id)
        {
			var foundMember = await _dbContext.Members.FindAsync(id);

			return View(foundMember);
        }

		[HttpPost]
		public async Task<IActionResult> Update(Member memberToUpdate)
		{
			if (ModelState.IsValid) {

				var member = await _dbContext.Members.FindAsync(memberToUpdate.Id);

				if (member != null) {

					member.FirstName = memberToUpdate.FirstName;
					member.LastName = memberToUpdate.LastName;
					member.Email = memberToUpdate.Email;
					member.Phone = memberToUpdate.Phone;

					await _dbContext.SaveChangesAsync();
				}
			}
			else {
				return View(memberToUpdate);
			}

			return RedirectToAction("Index");
		}
    }
}
