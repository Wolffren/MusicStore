using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Web.Infrastructure
{
    using AutoMapper;
    using Models.Entity;
    using Models.ViewModels.Album;
    using Models.ViewModels.Artist;
    using Models.ViewModels.Genres;
    using Models.ViewModels.Order;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Album, AlbumOverviewVm>();
            CreateMap<Album, AlbumDetailsVm>();

            CreateMap<Genre, GenreAlbumsVm>();

            CreateMap<Artist,ArtistNameVm>();

            CreateMap<OrderInputVm, Order>();
        }
    }
}
