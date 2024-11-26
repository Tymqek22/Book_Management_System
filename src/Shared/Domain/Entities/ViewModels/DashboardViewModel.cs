using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewModels
{
	public class DashboardViewModel
	{
		public BorrowStatisticsDto DailyStats { get; set; }
		public BorrowStatisticsDto MonthlyStats { get; set; }
		public BorrowStatisticsDto YearlyStats { get; set; }

		public IEnumerable<Book>? TopBorrowedBooks { get; set; }
		public IEnumerable<Member>? TheMostActiveMembers { get; set; }

	}
}
