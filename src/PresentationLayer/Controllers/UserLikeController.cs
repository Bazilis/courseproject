using BLL.Interfaces;
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
    }
}
