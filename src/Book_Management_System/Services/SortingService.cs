﻿using Book_Management_System.Services.Interfaces;

namespace Book_Management_System.Services
{
	public class SortingService : ISortingService
	{
		public IEnumerable<T> Sort<T>(IEnumerable<T> records,Func<T,object> sortBy)
		{
			return records.OrderBy(sortBy);
		}
	}
}
