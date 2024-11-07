using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IMemberService
	{
		void RegisterMember(Member newMember);
		void DeleteMember(int id);
		void UpdateMemberDetails(Member updatedMember);
		List<Member> GetAllMembers();
		Member GetMember(int id);
	}
}
