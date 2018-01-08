using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Models.ViewModels.Genres
{
    using Album;

    public class GenreAlbumsVm
    {
        public string Name { get; set; }

        public List<AlbumOverviewVm> Albums { get; set; }
        public string GenreIconUrl { get; set; }

        public string Description { get; set; }
    }
}
