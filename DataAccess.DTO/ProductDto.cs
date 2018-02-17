using System;

namespace DataAccess.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public decimal Weight { get; set; }
        public decimal Size { get; set; }
        public decimal Length { get; set; }
        public string PackingDetails { get; set; }
        public string Application { get; set; }
        public string MOQ { get; set; }
        public string DeliveryTime { get; set; }
    }
}
