using Book_Management_System.DTO;
using Book_Management_System.Repositories.Interfaces;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Repositories
{
	public class MemberRepository : IMemberRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public MemberRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Member> GetById(int id)
		{
			return await _dbContext.Members.FindAsync(id);
		}

		public async Task Insert(Member member)
		{
			await _dbContext.Members.AddAsync(member);
		}

		public void Delete(Member member)
		{
			_dbContext.Members.Remove(member);
		}

		public async Task Update(Member member)
		{
			var currentMember = await this.GetById(member.Id);

			if (currentMember != null) {

				currentMember.FirstName = member.FirstName;
				currentMember.LastName = member.LastName;
				currentMember.Email = member.Email;
				currentMember.Phone = member.Phone;
			}
		}

		public async Task Save()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Member>> GetAll()
		{
			return await _dbContext.Members.ToListAsync();
		}

		public async Task<IEnumerable<MemberStatsDto>> GetAllWithStats()
		{
			return await _dbContext.Members
				.Select(m => new MemberStatsDto
				{
					Member = m,
					BooksBorrowed = _dbContext.BorrowRecords.Count(br => br.MemberId == m.Id)
				})
				.ToListAsync();
		}

		public async Task<IEnumerable<MemberDto>> PopulateMembers()
		{
			return await _dbContext.Members
				.Select(m => new MemberDto
				{
					Id = m.Id,
					FullName = m.FirstName + " " + m.LastName
				})
				.OrderBy(d => d.FullName)
				.ToListAsync();
		}
	}
}
