using DataAccess.DTO;

namespace DataAccess.Factories
{
    public class ProductFactory
    {
        public ProductFactory()
        {
                
        }

        public ProductDto CreateProduct(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price ?? 0,
                Material = product.Material,
                Color = product.Color,
                Weight = product.Weight ?? 0,
                Size = product.Size ?? 0,
                Length = product.Length ?? 0,
                PackingDetails = product.PackingDetails,
                Application = product.Application,
                MOQ = product.MOQ,
                DeliveryTime = product.DeliveryTime
            };
        }

        public Product CreateProduct(ProductDto product)
        {
            return new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Material = product.Material,
                Color = product.Color,
                Weight = product.Weight,
                Size = product.Size,
                Length = product.Length,
                PackingDetails = product.PackingDetails,
                Application = product.Application,
                MOQ = product.MOQ,
                DeliveryTime = product.DeliveryTime
            };
        }
    }
}
