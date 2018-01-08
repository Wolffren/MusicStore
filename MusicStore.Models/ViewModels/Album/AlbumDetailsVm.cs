

namespace MusicStore.Models.ViewModels.Album
{
    using Artist;
    using Genres;

    public class AlbumDetailsVm
    {
        public int AlbumId { get; set; }

        public string Title { get; set; }

        public string AlbumArtUrl { get; set; }

        public GenreAlbumsVm Genre { get; set; }

        public ArtistNameVm Artist { get; set; }

        public decimal Price { get; set; }
    }
}
