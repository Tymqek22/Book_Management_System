using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Member
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the first name.")]
        public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter the last name.")]
		public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the email adress.")]
        [EmailAddress(ErrorMessage = "Please enter the valid email adress.")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Please enter the phone.")]
        [Phone(ErrorMessage = "Please enter the valid phone number.")]
		public string Phone { get; set; }

        public List<BorrowRecord>? BorrowRecords { get; set; }
    }
}
