

namespace MusicStore.Models.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        public int GenreId { get; set; }

        [Required]
        public string Name { get; set; }

        public string GenreIconUrl { get; set; }
        public string Description { get; set; }

        public List<Album> Albums { get; set; } = new List<Album>();


    }
}
