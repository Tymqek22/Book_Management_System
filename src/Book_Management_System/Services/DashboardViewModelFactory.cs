using Book_Management_System.Interfaces;
using Book_Management_System.ViewModels;

namespace Book_Management_System.Services
{
	public class DashboardViewModelFactory
	{
		private readonly IReportService _reportService;

		public DashboardViewModelFactory(IReportService reportService)
		{
			_reportService = reportService;
		}

		public async Task<DashboardViewModel> GetDashboardViewModel(string period)
		{
			DateTime today = DateTime.Now.Date;
			var viewModel = new DashboardViewModel();

			if (period == "daily") {

				viewModel.BorrowStats = await _reportService.GetPeriodicBorrowStats(today,today);
				viewModel.TopBorrowedBooks = await _reportService.GetMostPopularBooks(5);
				viewModel.TheMostActiveMembers = await _reportService.GetMostActiveMembers(5);
				viewModel.GenreStats = await _reportService.GetPeriodicGenreStats(today,today);
			}
			else if (period == "monthly") {

				DateTime monthlyStartDate = new DateTime(today.Year,today.Month,1);
				DateTime monthlyEndDate = new DateTime(today.Year,today.Month,DateTime.DaysInMonth(today.Year,today.Month));

				viewModel.BorrowStats = await _reportService.GetPeriodicBorrowStats(monthlyStartDate,monthlyEndDate);
				viewModel.TopBorrowedBooks = await _reportService.GetMostPopularBooks(5);
				viewModel.TheMostActiveMembers = await _reportService.GetMostActiveMembers(5);
				viewModel.GenreStats = await _reportService.GetPeriodicGenreStats(monthlyStartDate,monthlyEndDate);
			}
			else if (period == "yearly") {

				DateTime yearlyStartDate = new DateTime(today.Year,1,1);
				DateTime yearlyEndDate = new DateTime(today.Year,12,31);

				viewModel.BorrowStats = await _reportService.GetPeriodicBorrowStats(yearlyStartDate,yearlyEndDate);
				viewModel.TopBorrowedBooks = await _reportService.GetMostPopularBooks(5);
				viewModel.TheMostActiveMembers = await _reportService.GetMostActiveMembers(5);
				viewModel.GenreStats = await _reportService.GetPeriodicGenreStats(yearlyStartDate,yearlyEndDate);
			}
			else {

				return null;
			}

			return viewModel;
		}
	}
}
