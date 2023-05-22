namespace FlipCommerce.Model
{
    public class Cart
    {
        public Cart() 
        {
            this.Items=new HashSet<Item>();
        }
        public int Id { get; set; }
        public int CartTotal { get; set; }

        // Navigational Properties
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
