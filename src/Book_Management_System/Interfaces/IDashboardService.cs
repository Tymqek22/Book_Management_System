using Book_Management_System.ViewModels;

namespace Book_Management_System.Interfaces
{
	public interface IDashboardService
	{
		Task<DashboardViewModel> GetDashboardViewModelAsync(string period);
	}
}
