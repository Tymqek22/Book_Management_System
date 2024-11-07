using Domain.Entities;
using Domain.Interfaces;
using Domain.Persistence;

namespace Book_Management_System.Services
{
	public class MemberService : IMemberService
	{
		private readonly TempDB _context;

        public MemberService(TempDB context)
        {
			_context = context;
        }

        public void RegisterMember(Member newMember)
		{
			_context.Members.Add(newMember);
		}

		public void DeleteMember(int id)
		{
			var foundMember = _context.Members.FirstOrDefault(m => m.Id == id);

			if (foundMember != null) {

				_context.Members.Remove(foundMember);
			}
		}

		public void UpdateMemberDetails(Member updatedMember)
		{
			var foundMember = _context.Members.FirstOrDefault(m => m.Id == updatedMember.Id);

			if (foundMember != null) {

				int index = _context.Members.IndexOf(foundMember);
				_context.Members[index] = updatedMember;
			}
		}

		public List<Member> GetAllMembers()
		{
			return _context.Members;
		}

		public Member GetMember(int id)
		{
			return _context.Members.FirstOrDefault(m => m.Id == id);
		}
	}
}
