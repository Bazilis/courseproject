namespace DAL.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CollectionTypes CollectionType { get; set; }
        public string Description { get; set; }
        public DateTime CollectionCreationTime { get; set; } = DateTime.Now;
        public string? CollectionImageUrl { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}
