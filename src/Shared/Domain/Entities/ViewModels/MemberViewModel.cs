namespace Domain.Entities.ViewModels
{
	public class MemberViewModel
	{
        public Member Member { get; set; }
        public List<BorrowRecord> BorrowRecords { get; set; }
    }
}
