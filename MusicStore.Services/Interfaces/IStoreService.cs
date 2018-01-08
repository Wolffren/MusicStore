using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Services.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Entity;
    using Models.ViewModels.Album;
    using Models.ViewModels.Genres;

    public interface IStoreService
    {
        Task<IEnumerable<GenreAlbumsVm>> GetGenresAsync();
        IQueryable<AlbumOverviewVm> GetAlbumsByGenre(string genre);
        Task<AlbumDetailsVm> GetAlbumDetailsAsync(int id);
    }
}
