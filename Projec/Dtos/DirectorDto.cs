using MandD;
namespace Projec.Dtos
{
    public class DirectorDto
    {
        
        public int DirectorId { get; set; }

        public string Name { get; set; } 

        public int Age { get; set; }

        public string Nationality { get; set; }

        public string Email { get; set; }

        public  List<MovieDto>? Movies { get; set; }

        public List<string> Movieslist {  get; set; }
    }
}
