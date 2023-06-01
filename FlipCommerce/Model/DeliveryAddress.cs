namespace FlipCommerce.Model
{
    public class DeliveryAddress
    {
        public DeliveryAddress()
        {
            this.Orders = new HashSet<Order>();
        }
        public int  Id { get; set; }
        public string flat { get; set; }
        public string address1 { get; set; }
        public string Landmark { get; set; }
        public string city { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }
        public string MobNo { get; set; }

       // navigational properties
       public int? CustomerId { get; set; }
       public Customer? customer { get; set; }
       public ICollection<Order> Orders { get; set; }

    }
}
