using DAL.Entities;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class ItemImageRepository : GenericRepository<ItemImage>, IItemImageRepository
    {
        public ItemImageRepository(AppDbContext db) : base(db)
        {
        }
    }
}
