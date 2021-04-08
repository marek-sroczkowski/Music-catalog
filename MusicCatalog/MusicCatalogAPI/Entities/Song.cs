namespace MusicCatalogAPI.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublicationYear { get; set; }
        public double Duration { get; set; }

        public virtual Album Album { get; set; }
        public int AlbumId { get; set; }
    }
}
