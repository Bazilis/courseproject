using BLL.Interfaces;
using CollectionApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CollectionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICollectionService _collections;

        public HomeController(ICollectionService collections)
        {
            _collections = collections;
        }

        public IActionResult Index()
        {
            return View(_collections.GetAll());
        }

        [Authorize]
        public IActionResult ManageCollections(int userId)
        {
            return View(_collections.GetCollectionsByUserId(userId));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
