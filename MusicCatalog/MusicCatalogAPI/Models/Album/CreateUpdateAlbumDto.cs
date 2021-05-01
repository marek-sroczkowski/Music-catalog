using MusicCatalogAPI.Models.Artist;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models.Album
{
    public class CreateUpdateAlbumDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public CreateUpdateArtistDto Artist { get; set; }

        public string Version { get; set; }
    }
}
