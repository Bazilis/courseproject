using DAL.Entities;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private readonly AppDbContext _db;

        public ItemRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Item obj)
        {
            _db.Items?.Update(obj);
        }
    }
}
