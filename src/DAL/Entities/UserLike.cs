namespace DAL.Entities
{
    public class UserLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
