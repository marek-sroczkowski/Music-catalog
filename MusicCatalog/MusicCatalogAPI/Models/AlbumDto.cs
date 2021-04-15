using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        public string Version { get; set; }
        public string Supplier { get; set; }
        public ArtistDto Artist { get; set; }
    }
}
