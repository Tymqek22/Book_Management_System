using Book_Management_System.Interfaces;
using Book_Management_System.Services;
using Book_Management_System.ViewModels;
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

		public async Task<IActionResult> Index(string period)
		{
			ViewBag.CurrentPeriod = period;

			var factory = new DashboardViewModelFactory(_reportService);

			var viewModel = await factory.GetDashboardViewModel(period);

			return View(viewModel);
		}
	}
}
