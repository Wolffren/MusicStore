
namespace MusicStore.Services.Interfaces
{
    using System.Collections.Generic;
    using Models.ViewModels.Album;

    public interface IHomeService
    {
        IEnumerable<AlbumOverviewVm> GetTopSelling(int count);
    }
}
