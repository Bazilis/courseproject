using BLL.Dto;
using BLL.ExtensionMethods;
using BLL.Interfaces;
using DAL.Repository.Interfaces;
using Mapster;

namespace BLL.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collections;

        public CollectionService(ICollectionRepository collections)
        {
            _collections = collections;
        }

        public IEnumerable<CollectionDto> GetAll()
        {
            var collections = _collections.GetAll(orderBy: q => q.OrderByDescending(c => c.CollectionCreationTime));

            return collections.Adapt<IEnumerable<CollectionDto>>();
        }

        public IEnumerable<CollectionDto> GetCollectionsByUserId(int userId)
        {
            var collections = _collections.GetAll(
                filter: c => c.AppUserId == userId,
                orderBy: q => q.OrderByDescending(c => c.CollectionCreationTime));

            return collections.Adapt<IEnumerable<CollectionDto>>();
        }

        public IEnumerable<CollectionDto> GetCollectionsSortedFiltered(int userId, string sortParam, string filterName, string filterValue)
        {
            var collections = _collections.GetAll(filter: c => c.AppUserId == userId);

            var sortedCollections = sortParam switch
            {
                "name" => collections.OrderBy(c => c.Name),
                "type" => collections.OrderBy(c => c.CollectionType),
                "date" => collections.OrderBy(c => c.CollectionCreationTime),
                "date_desc" => collections.OrderByDescending(c => c.CollectionCreationTime),
                _ => collections.OrderByDescending(c => c.CollectionCreationTime),
            };

            var filteredCollections = filterName switch
            {
                "name" => sortedCollections.Where(c => c.Name == filterValue),
                "type" => sortedCollections.Where(c => c.CollectionType == filterValue.ToCollectionTypesEnum()),
                _ => sortedCollections,
            };

            return filteredCollections.Adapt<IEnumerable<CollectionDto>>();
        }

        public void DeleteCollectionById(int collectionId)
        {
            var collection = _collections.FirstOrDefault(filter: c => c.Id == collectionId);
            if (collection != null)
            {
                _collections.Remove(collection);
                _collections.Save();
            }
        }
    }
}
