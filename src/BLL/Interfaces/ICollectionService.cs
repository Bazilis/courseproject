using BLL.Dto;

namespace BLL.Interfaces
{
    public interface ICollectionService
    {
        IEnumerable<CollectionDto> GetAll();
        IEnumerable<CollectionDto> GetCollectionsByUserId(int userId);
        IEnumerable<CollectionDto> GetCollectionsSortedFiltered(int userId, string sortParam, string filterName, string filterValue);
        Task AddCollection(CollectionDto collectionDto);
        Task DeleteCollectionById(int collectionId);
    }
}
