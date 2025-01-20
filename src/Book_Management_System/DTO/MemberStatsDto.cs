using Domain.Entities;

namespace Book_Management_System.DTO
{
	public class MemberStatsDto
	{
		public Member Member { get; set; }
		public int BooksBorrowed { get; set; }
	}
}
