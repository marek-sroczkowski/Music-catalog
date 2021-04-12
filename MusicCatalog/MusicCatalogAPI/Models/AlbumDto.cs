using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public int PublicationYear { get; set; }
        public string Supplier { get; set; }
    }
}
