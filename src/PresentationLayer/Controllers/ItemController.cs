using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _items;

        public ItemController(IItemService items)
        {
            _items = items;
        }

        public IActionResult Details(int itemId)
        {
            return View(_items.GetItemById(itemId));
        }

        public IActionResult Delete(int collectionId, int itemId)
        {
            _items.DeleteItemById(itemId);
            return RedirectToAction("ManageItems", "Collection", new { collectionid = collectionId });
        }
    }
}
