namespace FlipCommerce.Model
{
    public class Seller:Person
    {
        public Seller() 
        {
            this.Products=new HashSet<Product>();
        }
        public override string ToString()
        {
            string ans = "{" +
                "\nname:" + this.Name +
                "\nAge:" + this.Age+
                "\ngender: "+this.gender+
                "\n}";
            return ans;
        }
        // Navigational Properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
