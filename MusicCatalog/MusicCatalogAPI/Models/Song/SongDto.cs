namespace MusicCatalogAPI.Models
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublicationYear { get; set; }
        public double Duration { get; set; }
    }
}