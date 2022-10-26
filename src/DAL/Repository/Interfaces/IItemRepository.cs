using DAL.Entities;

namespace DAL.Repository.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        void Update(Item obj);
    }
}
