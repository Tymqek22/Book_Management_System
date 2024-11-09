using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Persistence
{
	// temporary class, later will be changed into sql database
	public class TempDB
	{
        public List<Book> Books { get; set; }
		public List<Member> Members { get; set; }
        public List<BorrowRecord> BorrowRecords { get; set; }

        public TempDB()
        {
            Books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Book 1",
                    Author = "Author 1",
                    Language = "English",
                    Borrowed = false
                },
				new Book
				{
					Id = 2,
					Title = "Book 2",
					Author = "Author 2",
					Language = "English",
					Borrowed = false
				},
				new Book
				{
					Id = 3,
					Title = "Book 3",
					Author = "Author 3",
					Language = "English",
					Borrowed = false
				}
			};

			Members = new List<Member>
			{
				new Member
				{
					Id = 1,
					FirstName = "Jack",
					LastName = "Rider",
					Email = "jackrider@gmail.com",
					Phone = "123456789",
					IsActive = true
				}
			};

			BorrowRecords = new();
        }
    }
}
