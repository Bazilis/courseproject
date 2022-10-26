namespace BLL.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Сontent { get; set; }
        public DateTime CommentCreationTime { get; set; } = DateTime.Now;
    }
}
