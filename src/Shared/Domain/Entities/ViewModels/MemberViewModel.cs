using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewModels
{
	public class MemberViewModel
	{
        public Member Member { get; set; }
        public List<BorrowRecord> BorrowRecords { get; set; }
    }
}
