using System.Collections.Generic;

namespace MusicCatalogAPI.Entities
{
    public class Supplier : User
    {
        public string Name { get; set; }
        public Role Role { get; set; } = Role.MusicSupplier;

        public virtual List<Album> Albums { get; set; }
    }
}
