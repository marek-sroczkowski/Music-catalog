using System.Collections.Generic;

namespace MusicCatalogAPI.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }

        public virtual Supplier Supplier { get; set; }
        public int SupplierId { get; set; }

        public virtual Artist Artist { get; set; }
        public int ArtistId { get; set; }

        public virtual List<Song> Songs { get; set; }
    }
}