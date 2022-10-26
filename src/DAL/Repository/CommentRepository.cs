using DAL.Entities;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _db;

        public CommentRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
