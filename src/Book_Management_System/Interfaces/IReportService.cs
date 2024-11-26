using Domain.DTO;

namespace Book_Management_System.Interfaces
{
	public interface IReportService
	{
		Task<BorrowStatisticsDto> GetPeriodicBorrowStats(DateTime startDate,DateTime endDate);
	}
}
