using FlipCommerce.Enums;

namespace FlipCommerce.Model
{
    public class Order
    {
        public Order()
        {
            this.Items = new HashSet<Item>();
        }
        public int Id { get; set; }
        
        public string OrderNo { get; set; }// this will be unique everytime(uuid)
        // string uuid=Guid.NewGuid().ToString(); 
        public int OrderValue { get; set; }
        public DateTime OrderDate { get; set; }
        public string CardUsed { get; set; }
        public OrderStatus Status { get; set; }

        // Navigatinal Properties
        public int CustomerId { get; set; }
        public Customer customer { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
