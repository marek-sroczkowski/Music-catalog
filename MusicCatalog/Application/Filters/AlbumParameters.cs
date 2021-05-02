using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    public class AlbumParameters : QueryStringParameters
    {
        public string ArtistName { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
    }
}
