namespace MusicStore.Services.Implementations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Interfaces;
    using Models.ViewModels.Album;

    public class HomeService : IHomeService
    {
        private readonly MusicStoreDbContext _db;
        private readonly IMapper _mapper;
        public HomeService(MusicStoreDbContext db,IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }
        public IEnumerable<AlbumOverviewVm> GetTopSelling(int count)
        {
            var topSelling = this._db.Albums
                .OrderByDescending(a => a.OrderDetails.Count)
                .Take(count)
                .ToList();

            var result = this._mapper.Map<IEnumerable<AlbumOverviewVm>>(topSelling);
            return result;
        }
    }
}
