namespace BLL.Interfaces
{
    public interface IUserLikeService
    {
        void AddUserLike(int userId, int itemId);

        bool IsUserHasLike(int userId, int itemId);
    }
}
