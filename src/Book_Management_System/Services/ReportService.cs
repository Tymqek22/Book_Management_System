using Book_Management_System.DTO;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Book_Management_System.Services.Interfaces;

namespace Book_Management_System.Services
{
	public class ReportService : IReportService
	{
		private readonly ApplicationDbContext _dbContext;

		public ReportService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<BorrowStatisticsDto> GetPeriodicBorrowStats(DateTime startDate,DateTime endDate)
		{
			int totalBorrowed = await _dbContext.BorrowRecords
				.Where(br => br.StartDate >= startDate && br.StartDate <= endDate)
				.CountAsync();

			int totalReturned = await _dbContext.BorrowRecords
				.Where(br => br.ReturnDate >= startDate && br.ReturnDate <= endDate)
				.CountAsync();

			int totalOverdue = await _dbContext.BorrowRecords
				.Where(br => br.IsOverdue)
				.CountAsync();

			int currentlyBorrowed = await _dbContext.BorrowRecords
				.Where(br => br.IsActive)
				.CountAsync();

			return new BorrowStatisticsDto
			{
				TotalBooksBorrowed = totalBorrowed,
				TotalBooksReturned = totalReturned,
				TotalBooksOverdue = totalOverdue,
				CurrentlyBorrowedBooks = currentlyBorrowed
			};
		}

		public async Task<List<GenreStatisticsDto>> GetPeriodicGenreStats(DateTime startDate, DateTime endDate)
		{
			int totalBorrowedBooks = await _dbContext.BorrowRecords
				.Where(br => br.IsActive && br.StartDate >= startDate && br.StartDate <= endDate)
				.CountAsync();

			return await _dbContext.BorrowRecords
				.Where(br => br.IsActive && br.StartDate >= startDate && br.StartDate <= endDate)
				.Include(br => br.Book)
				.ThenInclude(b => b.Genre)
				.GroupBy(b => b.Book.Genre)
				.Select(grp => new GenreStatisticsDto
				{
					Genre = grp.Key,
					Percentage = (int)Math.Round(((double)grp.Count() / totalBorrowedBooks) * 100)
				})
				.OrderByDescending(s => s.Percentage)
				.ToListAsync();
		}

		public async Task<List<BookBorrowedDto>> GetMostPopularBooks(int limit)
		{
			return await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.Where(br => br.Book != null)
				.GroupBy(b => b.Book)
				.Select(grp => new BookBorrowedDto
				{
					Book = grp.Key,
					BooksBorrowed = grp.Count()
				})
				.OrderByDescending(r => r.BooksBorrowed)
				.Take(limit)
				.ToListAsync();
		}

		public async Task<List<Member>> GetMostActiveMembers(int limit)
		{
			return await _dbContext.BorrowRecords
				.Include(m => m.Member)
				.Where(br => br.Member != null)
				.GroupBy(m => m.Member)
				.Select(grp => new
				{
					Member = grp.Key,
					BooksBorrowed = grp.Count()
				})
				.OrderByDescending(member => member.BooksBorrowed)
				.Select(r => r.Member)
				.Take(limit)
				.ToListAsync();
		}
	}
}
