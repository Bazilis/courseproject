namespace DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Сontent { get; set; }
        public DateTime CommentCreationTime { get; set; } = DateTime.Now;

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
