using Microsoft.AspNetCore.Http;

namespace BLL.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ItemCreationTime { get; set; }
        public int CollectionId { get; set; }
        public int? ItemImageId { get; set; }
        public string? ItemImageUrl { get; set; }
        public ICollection<TagDto>? Tags { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
        public ICollection<UserLikeDto>? UsersLikes { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}
