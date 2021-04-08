using System.Collections.Generic;

namespace MusicCatalogAPI.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public virtual List<Album> Albums { get; set; }
    }
}
