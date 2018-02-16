using DataAccess.Contracts;
using System;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SafetyInventoryEntities _context;
        public ProductRepository(SafetyInventoryEntities context)
        {
            _context = context;
        }
        public IQueryable<DataAccess.Product> GetProducts()
        {
            return _context.Products;
        }

        public DataAccess.Product GetProduct(Guid id)
        {
            return _context.Products.FirstOrDefault(e => e.Id == id);
        }

        public RepositoryActionResult<DataAccess.Product> InsertProduct(DataAccess.Product e)
        {
            try
            {
                _context.Products.Add(e);
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Product>(e, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Product>(e, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<DataAccess.Product> UpdateProduct(DataAccess.Product e)
        {
            try
            {
                var existingProduct = _context.Products.FirstOrDefault(p => p.Id == e.Id);

                if (existingProduct == null)
                {
                    return new RepositoryActionResult<Product>(e, RepositoryActionStatus.NotFound);
                }

                _context.Entry(existingProduct).State = EntityState.Detached;
                
                _context.Products.Attach(e);

                _context.Entry(e).State = EntityState.Modified;


                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Product>(e, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Product>(e, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<DataAccess.Product> DeleteProduct(Guid Id)
        {
            try
            {
                var exp = _context.Products.FirstOrDefault(e => e.Id == Id);
                if (exp != null)
                {
                    _context.Products.Remove(exp);
                    _context.SaveChanges();
                    return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
