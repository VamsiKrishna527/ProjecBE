using MandD;
namespace Projec.Dtos
{
    public class MovieDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal Budget { get; set; }

        public string? Genre { get; set; }

        public decimal? Collections { get; set; }

    }
}
