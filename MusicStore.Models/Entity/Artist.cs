namespace MusicStore.Models.Entity
{
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        public int ArtistId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
