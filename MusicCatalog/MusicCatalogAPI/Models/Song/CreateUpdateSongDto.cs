using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models.Song
{
    public class CreateUpdateSongDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public double Duration { get; set; }
    }
}
