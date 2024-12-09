using Book_Management_System.Interfaces;
using Book_Management_System.ViewModels;

namespace Book_Management_System.Services
{
	public class DashboardService : IDashboardService
	{
		private readonly IReportService _reportService;

		public DashboardService(IReportService reportService)
		{
			_reportService = reportService;
		}

		public async Task<DashboardViewModel> GetDashboardViewModelAsync(string period)
		{
			var (startDate, endDate) = this.GetDateRange(period);

			var borrowStats = await _reportService.GetPeriodicBorrowStats(startDate,endDate);
			var topBorrowedBooks = await _reportService.GetMostPopularBooks(5);
			var theMostActiveMembers = await _reportService.GetMostActiveMembers(5);
			var genreStats = await _reportService.GetPeriodicGenreStats(startDate,endDate);

			return new DashboardViewModel
			{
				BorrowStats = borrowStats,
				TopBorrowedBooks = topBorrowedBooks,
				TheMostActiveMembers = theMostActiveMembers,
				GenreStats = genreStats
			};
		}

		private (DateTime startDate, DateTime endDate) GetDateRange(string period)
		{
			DateTime today = DateTime.Today;

			switch (period) {

				case "daily":
					return (today, today);

				case "monthly":
					return (new DateTime(today.Year,today.Month,1),
						new DateTime(today.Year,today.Month,DateTime.DaysInMonth(today.Year,today.Month)));

				case "yearly":
					return (new DateTime(today.Year,1,1), new DateTime(today.Year,12,31));
			}

			return (today, today);
		}
	}
}
