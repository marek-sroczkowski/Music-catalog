using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models
{
    public class ArtistDto
    {
        [Required]
        public string Name { get; set; }
    }
}
