using BLL.Dto;
using BLL.ExtensionMethods;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;
using Mapster;

namespace BLL.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collections;
        private readonly ICollectionImageRepository _images;
        private readonly ICloudinaryImageService _cloudinaryImageService;

        public CollectionService(
            ICollectionRepository collections,
            ICollectionImageRepository images,
            ICloudinaryImageService cloudinaryImageService)
        {
            _collections = collections;
            _images = images;
            _cloudinaryImageService = cloudinaryImageService;
        }

        public IEnumerable<CollectionDto> GetAll()
        {
            var collections = _collections.GetAll(
                orderBy: q => q.OrderByDescending(c => c.CollectionCreationTime),
                includeProperties: "CollectionImage");

            return collections.Adapt<IEnumerable<CollectionDto>>();
        }

        public IEnumerable<CollectionDto> GetCollectionsByUserId(int userId)
        {
            var collections = _collections.GetAll(
                filter: c => c.AppUserId == userId,
                orderBy: q => q.OrderByDescending(c => c.CollectionCreationTime),
                includeProperties: "CollectionImage");

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

        public async Task AddCollection(CollectionDto collectionDto)
        {
            var collection = collectionDto.Adapt<Collection>();
            collection.CollectionCreationTime = DateTime.Now;
            _collections.Add(collection);
            _collections.Save();

            if (collectionDto.FormFile != null)
            {
                var uploadResult = await _cloudinaryImageService.AddImageToCloudinaryAsync(collectionDto.FormFile);
                var collectionImage = new CollectionImage
                {
                    CollectionId = collection.Id,
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.Url.ToString()
                };
                _images.Add(collectionImage);
                _images.Save();
                collection.CollectionImageId = collectionImage.Id;
                _collections.Save();
            }
        }

        public async Task DeleteCollectionById(int collectionId)
        {
            var collection = _collections.FirstOrDefault(
                filter: c => c.Id == collectionId,
                includeProperties: "CollectionImage");
            if (collection != null)
            {
                if(collection.CollectionImage != null)
                    await _cloudinaryImageService
                        .DeleteImageFromCloudinaryAsync(collection.CollectionImage.PublicId);
                _collections.Remove(collection);
                _collections.Save();
            }
        }
    }
}
