namespace DAL.Entities
{
    public class CollectionImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
