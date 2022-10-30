using BLL.Dto;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;
using Mapster;

namespace BLL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _items;
        private readonly IItemImageRepository _images;
        private readonly ICloudinaryImageService _cloudinaryImageService;

        public ItemService(IItemRepository items,
            IItemImageRepository images,
            ICloudinaryImageService cloudinaryImageService)
        {
            _items = items;
            _images = images;
            _cloudinaryImageService = cloudinaryImageService;
        }

        public ItemDto GetItemById(int itemId)
        {
            var item = _items.FirstOrDefault(
                filter: i => i.Id == itemId,
                includeProperties: "ItemImage,Comments,UsersLikes");

            return item.Adapt<ItemDto>();
        }

        public IEnumerable<ItemDto> GetItemsByCollectionId(int collectionId)
        {
            var items = _items.GetAll(
                filter: i => i.CollectionId == collectionId,
                orderBy: q => q.OrderByDescending(i => i.ItemCreationTime),
                includeProperties: "ItemImage,UsersLikes");

            return items.Adapt<IEnumerable<ItemDto>>();
        }

        public IEnumerable<ItemDto> GetItemsSortedFiltered(int collectionId, string sortParam, string filterName, string filterValue)
        {
            var items = _items.GetAll(
                filter: i => i.CollectionId == collectionId,
                includeProperties: "ItemImage,UsersLikes");

            var sortedItems = sortParam switch
            {
                "name" => items.OrderBy(i => i.Name),
                "date" => items.OrderBy(i => i.ItemCreationTime),
                "date_desc" => items.OrderByDescending(i => i.ItemCreationTime),
                _ => items.OrderByDescending(i => i.ItemCreationTime),
            };

            var filteredItems = filterName switch
            {
                "name" => sortedItems.Where(i => i.Name == filterValue),
                "tag_name" => sortedItems.Where(i => i.Tags.Any(t => t.Name == filterValue)),
                _ => sortedItems,
            };

            return filteredItems.Adapt<IEnumerable<ItemDto>>();
        }

        public async Task AddItem(ItemDto itemDto)
        {
            var item = itemDto.Adapt<Item>();
            item.ItemCreationTime = DateTime.Now;
            _items.Add(item);
            _items.Save();

            if (itemDto.FormFile != null)
            {
                var uploadResult = await _cloudinaryImageService.AddImageToCloudinaryAsync(itemDto.FormFile);
                var itemImage = new ItemImage
                {
                    ItemId = item.Id,
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.Url.ToString()
                };
                _images.Add(itemImage);
                _images.Save();
                item.ItemImageId = itemImage.Id;
                _items.Save();
            }
        }

        public async Task DeleteItemById(int itemId)
        {
            var item = _items.FirstOrDefault(
                filter: i => i.Id == itemId,
                includeProperties: "ItemImage");

            if(item != null)
            {
                if (item.ItemImage != null)
                    await _cloudinaryImageService
                        .DeleteImageFromCloudinaryAsync(item.ItemImage.PublicId);

                _items.Remove(item);
                _items.Save();
            }
        }
    }
}
