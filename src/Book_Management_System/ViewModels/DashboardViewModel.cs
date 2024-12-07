using Domain.Entities;
using Book_Management_System.DTO;

namespace Book_Management_System.ViewModels
{
	public class DashboardViewModel
	{
		public BorrowStatisticsDto DailyStats { get; set; }
		public BorrowStatisticsDto MonthlyStats { get; set; }
		public BorrowStatisticsDto YearlyStats { get; set; }

		public IEnumerable<Book>? TopBorrowedBooks { get; set; }
		public IEnumerable<Member>? TheMostActiveMembers { get; set; }
		public IEnumerable<GenreStatisticsDto>? GenreStats { get; set; }
	}
}
