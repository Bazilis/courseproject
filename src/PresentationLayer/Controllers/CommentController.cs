using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _comments;

        public CommentController(ICommentService comments)
        {
            _comments = comments;
        }

        [HttpPost]
        public IActionResult AddComment(int userId, int itemId, string content)
        {
            _comments.AddComment(userId, itemId, content);

            return RedirectToAction("Details", "Item", new { itemid = itemId });
        }
    }
}
