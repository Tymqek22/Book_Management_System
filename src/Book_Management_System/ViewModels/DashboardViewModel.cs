using Domain.Entities;
using Book_Management_System.DTO;

namespace Book_Management_System.ViewModels
{
	public class DashboardViewModel
	{
		public BorrowStatisticsDto BorrowStats { get; set; }

		public IEnumerable<BookBorrowedDto>? TopBorrowedBooks { get; set; }
		public IEnumerable<Member>? TheMostActiveMembers { get; set; }
		public IEnumerable<GenreStatisticsDto>? GenreStats { get; set; }
	}
}
