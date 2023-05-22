using FlipCommerce.Enums;

namespace FlipCommerce.Model
{
    public class Product
    {
        public Product()
        {
            this.Items=new HashSet<Item>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Category category { get; set; }
        public int Quantity { get; set; }
        public ProductStatus productStatus { get; set; }

        // Navigational Properties
        public int SellerId { get; set; }
        public Seller seller { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
