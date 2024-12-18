using Book_Management_System.ViewModels;

namespace Book_Management_System.Services.Interfaces
{
	public interface IDashboardService
	{
		Task<DashboardViewModel> GetDashboardViewModelAsync(string period);
	}
}
