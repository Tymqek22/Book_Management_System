using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Book
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public bool Borrowed { get; set; }

        public BorrowRecord? BorrowRecord { get; set; }
    }
}
