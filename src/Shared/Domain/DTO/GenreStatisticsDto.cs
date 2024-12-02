using Domain.Entities;

namespace Domain.DTO
{
	public class GenreStatisticsDto
	{
		public Genre Genre { get; set; }
		public int Percentage { get; set; }
	}
}
