using Book_Management_System.Repositories.Interfaces;
using Book_Management_System.Utilities;
using Book_Management_System.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management_System.Controllers
{
	public class MemberController : Controller
	{
		private readonly IBorrowRecordRepository _borrowRecordRepository;
		private readonly IMemberRepository _memberRepository;

        public MemberController(IBorrowRecordRepository borrowRecordRepository,IMemberRepository memberRepository)
        {
			_borrowRecordRepository = borrowRecordRepository;
			_memberRepository = memberRepository;
        }

		public async Task<IActionResult> Index(int pageNumber)
		{
			var members = await _memberRepository.GetAll();

			return View(PaginatedList<Member>.Create(members,pageNumber,10));
		}

		public async Task<IActionResult> Details(int id)
		{
			var member = await _memberRepository.GetById(id);

			var borrowRecords = await _borrowRecordRepository.GetMemberActiveRecords(member);

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

				await _memberRepository.Insert(newMember);
				await _memberRepository.Save();
			}
			else {
				return View(newMember);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			var member = await _memberRepository.GetById(id);

			if (member != null) {

				member.BorrowRecords = await _borrowRecordRepository.GetAllMemberRecords(member);

				if (!member.BorrowRecords.Any(br => br.IsActive)) {

					foreach (var borrow in member.BorrowRecords) {

						borrow.MemberId = null;
					}

					_memberRepository.Delete(member);
					await _memberRepository.Save();
				}
				else {

					var error = new ErrorViewModel
					{
						RequestId = "Cannot delete the member, has books borrowed."
					};

					return View("Error",error);
				}
				
			}

			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Update(int id)
        {
			var foundMember = await _memberRepository.GetById(id);

			return View(foundMember);
        }

		[HttpPost]
		public async Task<IActionResult> Update(Member memberToUpdate)
		{
			if (ModelState.IsValid) {

				await _memberRepository.Update(memberToUpdate);
				await _memberRepository.Save();
			}
			else {
				return View(memberToUpdate);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> BorrowHistory(int memberId)
		{
			var member = await _memberRepository.GetById(memberId);

			var borrowRecords = await _borrowRecordRepository.GetAllMemberRecords(member);

			var memberViewModel = new MemberViewModel
			{
				Member = member,
				BorrowRecords = borrowRecords
			};

			return View(memberViewModel);
		}
    }
}
