using BLL.Dto;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;
using Mapster;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _comments;

        public CommentService(ICommentRepository comments)
        {
            _comments = comments;
        }

        public void AddComment(int userId, int itemId, string content)
        {
            if(content != null)
            {
                var comment = new Comment()
                {
                    UserId = userId,
                    ItemId = itemId,
                    Сontent = content
                };
                _comments.Add(comment);
                _comments.Save();
            }
        }

        public IEnumerable<CommentDto> GetCommentsByItemId(int itemId)
        {
            var collections = _comments.GetAll(
                filter: c => c.ItemId == itemId,
                orderBy: q => q.OrderByDescending(c => c.CommentCreationTime));

            return collections.Adapt<IEnumerable<CommentDto>>();
        }
    }
}
