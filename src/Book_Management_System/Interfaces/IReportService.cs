using Book_Management_System.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Book_Management_System.Interfaces
{
	public interface IReportService
	{
		Task<BorrowStatisticsDto> GetPeriodicBorrowStats(DateTime startDate,DateTime endDate);
		Task<List<GenreStatisticsDto>> GetPeriodicGenreStats(DateTime startDate,DateTime endDate);
		Task<List<BookBorrowedDto>> GetMostPopularBooks(int limit);
		Task<List<Member>> GetMostActiveMembers(int limit);
		
	}
}
