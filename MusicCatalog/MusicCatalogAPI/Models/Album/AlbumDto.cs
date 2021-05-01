namespace MusicCatalogAPI.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Version { get; set; }
        public string Supplier { get; set; }
        public ArtistDto Artist { get; set; }
    }
}
