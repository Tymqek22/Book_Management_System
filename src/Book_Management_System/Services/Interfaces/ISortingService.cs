namespace Book_Management_System.Services.Interfaces
{
	public interface ISortingService
	{
		IEnumerable<T> Sort<T>(IEnumerable<T> records,Func<T,object> sortBy,bool ascending);
	}
}
