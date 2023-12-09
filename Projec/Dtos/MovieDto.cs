using MandD;
namespace Projec.Dtos
{
    public class MovieDto
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal Budget { get; set; }

        public string? Genre { get; set; }

        public decimal? Collections { get; set; }

        public int? DirectorId { get; set; }
    }
}
