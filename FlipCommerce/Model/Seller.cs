namespace FlipCommerce.Model
{
    public class Seller:Person
    {
        public Seller() 
        {
            this.Products=new HashSet<Product>();
        }
        // Navigational Properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
