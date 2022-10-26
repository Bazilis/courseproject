using BLL.Dto;
using BLL.Interfaces;
using DAL.Repository.Interfaces;
using Mapster;

namespace BLL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _items;

        public ItemService(IItemRepository items)
        {
            _items = items;
        }

        public ItemDto GetItemById(int itemId)
        {
            var item = _items.FirstOrDefault(
                filter: i => i.Id == itemId,
                includeProperties: "Comments,UsersLikes");

            return item.Adapt<ItemDto>();
        }

        public IEnumerable<ItemDto> GetItemsByCollectionId(int collectionId)
        {
            var items = _items.GetAll(
                filter: i => i.CollectionId == collectionId,
                orderBy: q => q.OrderByDescending(i => i.ItemCreationTime));

            return items.Adapt<IEnumerable<ItemDto>>();
        }

        public IEnumerable<ItemDto> GetItemsSortedFiltered(int collectionId, string sortParam, string filterName, string filterValue)
        {
            var items = _items.GetAll(filter: i => i.CollectionId == collectionId);

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

        public void DeleteItemById(int itemId)
        {
            var item = _items.FirstOrDefault(filter: i => i.Id == itemId);
            if(item != null)
            {
                _items.Remove(item);
                _items.Save();
            }
        }
    }
}
