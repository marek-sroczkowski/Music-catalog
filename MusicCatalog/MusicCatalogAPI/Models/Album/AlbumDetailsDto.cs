using MusicCatalogAPI.Models.Song;
using System.Collections.Generic;

namespace MusicCatalogAPI.Models
{
    public class AlbumDetailsDto
    {
        public int Id { get; set; }
        public ArtistDto Artist { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public int PublicationYear { get; set; }
        public string Supplier { get; set; }
        public List<SongDto> Songs{ get; set; }
    }
}
