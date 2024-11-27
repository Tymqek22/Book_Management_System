using Book_Management_System.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

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

			var stats = new BorrowStatisticsDto
			{
				TotalBooksBorrowed = totalBorrowed,
				TotalBooksReturned = totalReturned,
				TotalBooksOverdue = totalOverdue,
				CurrentlyBorrowedBooks = currentlyBorrowed
			};

			return stats;
		}

		public async Task<List<Book>> GetMostPopularBooks(int limit)
		{
			var books = await _dbContext.BorrowRecords
				.Include(b => b.Book)
				.GroupBy(b => b.Book)
				.Select(grp => new
				{
					Book = grp.Key,
					NumberOfBorrowed = grp.Count()
				})
				.OrderByDescending(r => r.NumberOfBorrowed)
				.Select(book => book.Book)
				.Take(limit)
				.ToListAsync();

			return books;
		}

		public async Task<List<Member>> GetMostActiveMembers(int limit)
		{
			var members = await _dbContext.BorrowRecords
				.Include(m => m.Member)
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

			return members;
		}
	}
}
