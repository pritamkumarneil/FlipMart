namespace FlipCommerce.Model
{
    public class ProductImage
    {

        public int Id { get; set; }
        public string ImageUrl { get; set; }


        // navigational Properties
        public int? ProductId { get; set; }
        public virtual Product product { get; set; }
    }
}
