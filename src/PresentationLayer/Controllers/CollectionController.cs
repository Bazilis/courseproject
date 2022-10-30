using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IItemService _items;
        private readonly ICollectionService _collections;

        public CollectionController(
            IItemService items,
            ICollectionService collections)
        {
            _items = items;
            _collections = collections;
        }

        public IActionResult Details(int collectionId)
        {
            return View(_items.GetItemsByCollectionId(collectionId));
        }

        [Authorize]
        public IActionResult ManageItems(int collectionId, string sortParam, string filterName, string filterValue)
        {
            return View(_items.GetItemsSortedFiltered(collectionId, sortParam, filterName, filterValue));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CollectionDto collectionDto)
        {
            await _collections.AddCollection(collectionDto);
            return RedirectToAction("ManageCollections", "Home", new { userid = collectionDto.AppUserId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int collectionId, int userId)
        {
            await _collections.DeleteCollectionById(collectionId);
            return RedirectToAction("ManageCollections", "Home", new { userid = userId });
        }
    }
}
