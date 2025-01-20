namespace Book_Management_System.Utilities
{
	public class PaginatedList<T>
	{
		public List<T> Items { get; set; }
		public int TotalItems { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public string SortBy { get; set; }
		public bool Ascending { get; set; }

		public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string sortOption,bool ascending)
		{
			Items = items;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			PageIndex = pageIndex;
			SortBy = sortOption;
			Ascending = ascending;
		}

		public static PaginatedList<T> Create(IEnumerable<T> allItems,int pageIndex,int pageSize,string sortOption,bool ascending)
		{
			var items = allItems
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			int totalItems = allItems.Count();

			return new PaginatedList<T>(items,totalItems,pageIndex,pageSize,sortOption,ascending);
		}
	}
}
