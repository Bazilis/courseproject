namespace DAL.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ItemCreationTime { get; set; } = DateTime.Now;

        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
        public int? ItemImageId { get; set; }
        public ItemImage? ItemImage { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<UserLike>? UsersLikes { get; set; }
    }
}
