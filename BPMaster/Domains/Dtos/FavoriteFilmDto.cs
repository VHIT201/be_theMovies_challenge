using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class FavoriteFilmDto
    {
        [Required]
        public Guid userid { get; set; }
        [Required]
        public string movieid { get; set; }
        [Required]
        public string media_type { get; set; }

    }
}
