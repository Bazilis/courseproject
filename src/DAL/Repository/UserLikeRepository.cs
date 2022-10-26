using DAL.Entities;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class UserLikeRepository : GenericRepository<UserLike>, IUserLikeRepository
    {
        private readonly AppDbContext _db;

        public UserLikeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
