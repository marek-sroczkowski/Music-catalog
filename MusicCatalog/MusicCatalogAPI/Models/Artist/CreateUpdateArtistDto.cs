using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models.Artist
{
    public class CreateUpdateArtistDto
    {
        [Required]
        public string Name { get; set; }
    }
}
