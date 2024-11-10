﻿using System;
using System.Collections.Generic;
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

		public int MemberId { get; set; }
		public Member? Member { get; set; }

		public int BookId { get; set; }
		public Book? Book { get; set; }
    }
}
