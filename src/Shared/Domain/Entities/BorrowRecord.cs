using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class BorrowRecord
	{
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public bool IsActive { get; set; }
        public bool IsOverdue { get; set; }
        public decimal OverdueFine { get; set; }

        public int? MemberId { get; set; }
		public Member? Member { get; set; }

		public int? BookId { get; set; }
		public Book? Book { get; set; }
    }
}
