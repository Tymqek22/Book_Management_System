using Domain.Entities;

namespace Book_Management_System.DTO
{
	public class GenreStatisticsDto
	{
		public Genre Genre { get; set; }
		public int Percentage { get; set; }
	}
}
