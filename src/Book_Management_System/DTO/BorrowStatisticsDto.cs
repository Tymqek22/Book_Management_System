namespace Book_Management_System.DTO
{
	public class BorrowStatisticsDto
	{
		public int TotalBooksBorrowed { get; set; }
		public int TotalBooksReturned { get; set; }
		public int TotalBooksOverdue { get; set; }
		public int CurrentlyBorrowedBooks { get; set; }
	}
}
