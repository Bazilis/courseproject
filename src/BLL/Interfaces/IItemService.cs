using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IItemService
    {
        ItemDto GetItemById(int itemId);
        IEnumerable<ItemDto> GetItemsByCollectionId(int collectionId);
        IEnumerable<ItemDto> GetItemsSortedFiltered(int collectionId, string sortParam, string filterName, string filterValue);
        Task AddItem(ItemDto itemDto);
        Task DeleteItemById(int itemId);
    }
}
