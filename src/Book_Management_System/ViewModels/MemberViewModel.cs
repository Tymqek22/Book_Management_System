using Domain.Entities;

namespace Book_Management_System.ViewModels
{
	public class MemberViewModel
	{
        public Member Member { get; set; }
        public List<BorrowRecord> BorrowRecords { get; set; }
    }
}
