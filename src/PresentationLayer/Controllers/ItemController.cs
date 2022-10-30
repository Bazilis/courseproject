using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Create(int collectionId)
        {
            return View(new ItemDto { CollectionId = collectionId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ItemDto itemDto)
        {
            await _items.AddItem(itemDto);
            return RedirectToAction("ManageItems", "Collection", new { collectionid = itemDto.CollectionId });
        }

        public async Task<IActionResult> Delete(int collectionId, int itemId)
        {
            await _items.DeleteItemById(itemId);
            return RedirectToAction("ManageItems", "Collection", new { collectionid = collectionId });
        }
    }
}
