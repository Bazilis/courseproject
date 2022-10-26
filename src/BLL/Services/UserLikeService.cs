using BLL.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;

namespace BLL.Services
{
    public class UserLikeService : IUserLikeService
    {
        private readonly IUserLikeRepository _userLikes;

        public UserLikeService(IUserLikeRepository userLikes)
        {
            _userLikes = userLikes;
        }

        public void AddUserLike(int userId, int itemId)
        {
            var userLike = new UserLike() { UserId = userId, ItemId = itemId };
            _userLikes.Add(userLike);
            _userLikes.Save();
        }

        public bool IsUserHasLike(int userId, int itemId)
        {
            var like = _userLikes.FirstOrDefault(
                filter: u => u.UserId == userId && u.ItemId == itemId);

            if (like == null)
                return false;

            return true;
        }
    }
}
