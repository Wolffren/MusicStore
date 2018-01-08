using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Services.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels.Album;
    using Models.ViewModels.Genres;

    public class StoreService : IStoreService
    {
        private readonly MusicStoreDbContext _db;
        private readonly IMapper _mapper;
        public StoreService(MusicStoreDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<GenreAlbumsVm>> GetGenresAsync()
        {
            var genres =  await this._db.Genres.ToListAsync();

            var result = this._mapper.Map<IEnumerable<GenreAlbumsVm>>(genres);

            return result;
        }

        public IQueryable<AlbumOverviewVm> GetAlbumsByGenre(string genre)
        {

            var result = from m in this._db.Albums
                where m.Genre.Name == genre
                select new AlbumOverviewVm
                {
                    AlbumArtUrl = m.AlbumArtUrl,
                    AlbumId = m.AlbumId,
                    Title = m.Title
                };
            return result;

        }

        public async Task<AlbumDetailsVm> GetAlbumDetailsAsync(int id)
        {
            var album = await this._db.Albums
                .Where(a => a.AlbumId == id)
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync();

            var result = this._mapper.Map<AlbumDetailsVm>(album);

            return result;
        }
    }
}
