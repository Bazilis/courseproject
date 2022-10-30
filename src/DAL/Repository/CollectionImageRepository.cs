using DAL.Entities;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class CollectionImageRepository : GenericRepository<CollectionImage>, ICollectionImageRepository
    {
        public CollectionImageRepository(AppDbContext db) : base(db)
        {
        }
    }
}
