using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models
{
    public class SongDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public double Duration { get; set; }
    }
}