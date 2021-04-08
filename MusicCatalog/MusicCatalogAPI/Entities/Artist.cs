using System.Collections.Generic;

namespace MusicCatalogAPI.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Album> Albums { get; set; }
    }
}
