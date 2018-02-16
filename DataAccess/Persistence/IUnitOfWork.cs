using DataAccess.Contracts;

namespace DataAccess.Persistence
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        void Complete();
    }
}