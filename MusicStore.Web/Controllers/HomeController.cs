

namespace MusicStore.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using MusicStore.Services.Interfaces;

    public class HomeController : Controller
    {
        private readonly IHomeService _service;

        public HomeController(IHomeService service)
        {
            this._service = service;
        }
        public IActionResult Index()
        {
            var albums = this._service.GetTopSelling(15);
            return View(albums);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
