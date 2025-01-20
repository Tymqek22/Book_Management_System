using Book_Management_System.DTO;
using Domain.Entities;

namespace Book_Management_System.Repositories.Interfaces
{
	public interface IMemberRepository
	{
		Task<Member> GetById(int id);
		Task Insert(Member member);
		void Delete(Member member);
		Task Update(Member member);
		Task Save();

		Task<IEnumerable<Member>> GetAll();
		Task<IEnumerable<MemberStatsDto>> GetAllWithStats();
		Task<IEnumerable<MemberDto>> PopulateMembers();
	}
}
