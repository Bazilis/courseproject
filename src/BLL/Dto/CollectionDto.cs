using Microsoft.AspNetCore.Http;

namespace BLL.Dto
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CollectionType { get; set; }
        public string Description { get; set; }
        public DateTime CollectionCreationTime { get; set; }
        public int AppUserId { get; set; }
        public int? CollectionImageId { get; set; }
        public string? CollectionImageUrl { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}
