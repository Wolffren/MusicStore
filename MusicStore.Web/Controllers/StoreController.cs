using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MusicStore.Web.Controllers
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels.Album;
    using Models.ViewModels.Genres;
    using MusicStore.Services.Interfaces;

    public class StoreController : Controller
    {
        private readonly IStoreService _service;

        public StoreController(IStoreService service)
        {
            this._service = service;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await this._service.GetGenresAsync();
            return View(genres);
        }

        public async Task<IActionResult> Browse(string genre, int? page)
        {
            var albums =  this._service.GetAlbumsByGenre(genre);

            ViewData["CurrentGenre"] = genre;
            if (!albums.Any())
            {
                return NotFound();
            }
            int pageSize = 15;
            return View(await PaginatedList<AlbumOverviewVm>.CreateAsync(albums.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await this._service.GetAlbumDetailsAsync(id);

            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
    }
}