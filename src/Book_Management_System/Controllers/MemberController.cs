using Domain.Entities;
using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(Member newMember)
		{
			await _dbContext.Members.AddAsync(newMember);
			await _dbContext.SaveChangesAsync();

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
			var member = await _dbContext.Members.FindAsync(memberToUpdate.Id);

			if (member != null) {

				member.FirstName = memberToUpdate.FirstName;
				member.LastName = memberToUpdate.LastName;
				member.Email = memberToUpdate.Email;
				member.Phone = memberToUpdate.Phone;

				await _dbContext.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}
    }
}
