using Book_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management_System.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardService _dashboardService;
		private readonly IBorrowService _borrowService;

		//TODO: reports to files

		public DashboardController(IDashboardService dashboardService, IBorrowService borrowService)
		{
			_dashboardService = dashboardService;
			_borrowService = borrowService;
		}

		public async Task<IActionResult> Index(string period)
		{
			ViewBag.CurrentPeriod = period;

			await _borrowService.TrackOverdue();
			var viewModel = await _dashboardService.GetDashboardViewModelAsync(period);

			return View(viewModel);
		}
	}
}
