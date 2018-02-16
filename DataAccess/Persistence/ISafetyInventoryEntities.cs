using System.Data.Entity;

namespace DataAccess.Persistence
{
    public interface ISafetyInventoryEntities
    {
        DbSet<AspNetUser> AspNetUsers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductGroup> ProductGroups { get; set; }
    }
}