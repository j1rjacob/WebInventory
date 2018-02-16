using System;

namespace DataAccess.Contracts
{
    public interface IProductRepository
    {
        System.Linq.IQueryable<Product> GetProducts();
        Product GetProduct(Guid Id);
        RepositoryActionResult<Product> InsertProduct(Product e);
        RepositoryActionResult<Product> UpdateProduct(Product e);
        RepositoryActionResult<Product> DeleteProduct(Guid Id);
    }
}
