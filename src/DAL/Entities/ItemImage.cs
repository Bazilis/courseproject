namespace DAL.Entities
{
    public class ItemImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
