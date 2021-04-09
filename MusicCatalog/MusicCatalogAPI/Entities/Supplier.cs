using System.Collections.Generic;

namespace MusicCatalogAPI.Entities
{
    public class Supplier : User
    {
        public string Name { get; set; }

        public virtual List<Album> Albums { get; set; }

        public Supplier()
        {
            Role = Role.MusicSupplier;
        }
    }
}
