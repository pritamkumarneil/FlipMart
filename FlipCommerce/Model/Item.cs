namespace FlipCommerce.Model
{
    public class Item
    {
        public int Id { get; set; }
        public int RequiredQuantity { get; set; }

        //NavigationalProperties
        public int? CartId { get; set; }
        public Cart? cart { get; set; }
        public int? OrderId { get; set; }
        public Order? order { get; set; }
        public int? ProductId { get; set; }
        public Product? product { get; set; }
    }
}
