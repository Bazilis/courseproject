using DAL.Entities;

namespace DAL.Repository.Interfaces
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        void Update(Collection obj);
    }
}
