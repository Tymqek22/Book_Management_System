using Domain.Entities;

namespace Book_Management_System.DTO
{
	public class BookBorrowedDto
	{
		public Book Book { get; set; }
		public int BooksBorrowed { get; set; }
	}
}
