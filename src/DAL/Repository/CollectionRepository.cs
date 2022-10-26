using DAL.Entities;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository
    {
        private readonly AppDbContext _db;

        public CollectionRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Collection obj)
        {
            _db.Collections?.Update(obj);
        }
    }
}
