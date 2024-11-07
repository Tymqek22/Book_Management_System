using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Book_Management_System.Controllers
{
	public class MemberController : Controller
	{
		private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
			_memberService = memberService;
        }

		public IActionResult Index()
		{
			return View(_memberService.GetAllMembers());
		}

        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(Member newMember)
		{
			_memberService.RegisterMember(newMember);
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			_memberService.DeleteMember(id);
			return RedirectToAction("Index");
		}

        public IActionResult Update(int id)
        {
			var foundMember = _memberService.GetMember(id);

			return View(foundMember);
        }

		[HttpPost]
		public IActionResult Update(Member memberToUpdate)
		{
			_memberService.UpdateMemberDetails(memberToUpdate);
			return RedirectToAction("Index");
		}
    }
}
