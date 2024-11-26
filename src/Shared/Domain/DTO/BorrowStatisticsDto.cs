using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
	public class BorrowStatisticsDto
	{
		public int TotalBooksBorrowed { get; set; }
		public int TotalBooksReturned { get; set; }
		public int TotalBooksOverdue { get; set; }
		public int CurrentlyBorrowedBooks { get; set; }
	}
}
