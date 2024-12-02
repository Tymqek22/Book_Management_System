using Book_Management_System.Interfaces;
using Domain.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management_System.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IReportService _reportService;

		//TODO: reports to files

		public DashboardController(IReportService reportService)
		{
			_reportService = reportService;
		}

		public async Task<IActionResult> Index()
		{
			DateTime today = DateTime.Now.Date;

			DateTime monthlyStartDate = new DateTime(today.Year,today.Month,1);
			DateTime monthlyEndDate = new DateTime(today.Year,today.Month,DateTime.DaysInMonth(today.Year,today.Month));

			DateTime yearlyStartDate = new DateTime(today.Year,1,1);
			DateTime yearlyEndDate = new DateTime(today.Year,12,31);

			var viewModel = new DashboardViewModel
			{
				DailyStats = await _reportService.GetPeriodicBorrowStats(today,today),
				MonthlyStats = await _reportService.GetPeriodicBorrowStats(monthlyStartDate,monthlyEndDate),
				YearlyStats = await _reportService.GetPeriodicBorrowStats(yearlyStartDate,yearlyEndDate),
				TopBorrowedBooks = await _reportService.GetMostPopularBooks(5),
				TheMostActiveMembers = await _reportService.GetMostActiveMembers(5),
				GenreStats = await _reportService.GetPeriodicGenreStats(monthlyStartDate,monthlyEndDate)
			};

			return View(viewModel);
		}
	}
}
