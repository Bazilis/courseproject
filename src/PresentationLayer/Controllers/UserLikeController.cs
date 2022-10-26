using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class UserLikeController : Controller
    {
        private readonly IUserLikeService _userLikes;

        public UserLikeController(IUserLikeService userLikes)
        {
            _userLikes = userLikes;
        }

        public IActionResult AddUserLike(int userId, int itemId)
        {
            _userLikes.AddUserLike(userId, itemId);

            return RedirectToAction("Details", "Item", new { itemid = itemId });
        }

        // GET: UserLikeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserLikeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserLikeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserLikeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserLikeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserLikeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserLikeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserLikeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
