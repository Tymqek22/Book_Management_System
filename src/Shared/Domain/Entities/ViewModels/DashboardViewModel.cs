using Domain.DTO;

namespace Domain.Entities.ViewModels
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
