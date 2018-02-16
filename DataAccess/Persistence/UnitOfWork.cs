using DataAccess.Contracts;
using DataAccess.Repositories;

namespace DataAccess.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SafetyInventoryEntities _context;

        public IProductRepository Products { get; private set; }

        public UnitOfWork(SafetyInventoryEntities context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
