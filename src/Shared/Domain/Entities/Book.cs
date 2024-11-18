using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class Book
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the author.")]
        public string Author { get; set; }

		[Required(ErrorMessage = "Please enter the language.")]
		public string Language { get; set; }
        public bool IsAvailable { get; set; }

		[Required(ErrorMessage = "Please enter the quantity")]
        [Range(0,int.MaxValue,ErrorMessage = "Only positive numbers allowed")]
		public int Quantity { get; set; }

        public List<BorrowRecord>? BorrowRecord { get; set; }
    }
}
