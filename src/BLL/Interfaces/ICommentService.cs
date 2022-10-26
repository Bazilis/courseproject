using BLL.Dto;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        void AddComment(int userId, int itemId, string content);
        IEnumerable<CommentDto> GetCommentsByItemId(int itemId);
    }
}
