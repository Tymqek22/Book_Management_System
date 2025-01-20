using Book_Management_System.DTO;
using Book_Management_System.Repositories.Interfaces;
using Book_Management_System.Services.Interfaces;
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
		private readonly ISortingService _sortingService;

        public MemberController(IBorrowRecordRepository borrowRecordRepository,IMemberRepository memberRepository,
			ISortingService sortingService)
        {
			_borrowRecordRepository = borrowRecordRepository;
			_memberRepository = memberRepository;
			_sortingService = sortingService;
        }

		public async Task<IActionResult> Index(int pageNumber,string sortBy = "Name",bool ascending = true)
		{
			var members = await _memberRepository.GetAllWithStats();

			Func<MemberStatsDto,object> sortingOption = sortBy switch
			{
				"Name" => member => member.Member.LastName,
				"Popularity" => member => member.BooksBorrowed
			};

			var sortedMembers = _sortingService.Sort(members,sortingOption,ascending);

			return View(PaginatedList<MemberStatsDto>.Create(sortedMembers,pageNumber,10,sortBy,ascending));
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
