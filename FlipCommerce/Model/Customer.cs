﻿namespace FlipCommerce.Model
{
    public class Customer:Person
    {
        public Customer()
        {
            this.Cards = new HashSet<Card>();
            this.Orders=new HashSet<Order>();
            this.addresses = new HashSet<DeliveryAddress>();
        }

        //Navigational Properties

        public virtual ICollection<Card> Cards { get; set; }

        public virtual Cart cart { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<DeliveryAddress> addresses { get; set; }
    }
}
