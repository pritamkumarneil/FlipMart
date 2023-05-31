using FlipCommerce.Enums;

namespace FlipCommerce.Model
{
    public class Product
    {
        public Product()
        {
            this.Items=new HashSet<Item>();
            this.ProductImages=new HashSet<ProductImage>();
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

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
    // make another table for productImage//product entity will contain the list of product image
    // and product image table will contain the link of the product image and the foreign key of product
}
