using FlipCommerce.Enums;

namespace FlipCommerce.DTO.RequestDto
{
    public class ProductRequestDto
    {
        public string SellerMail { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public Category category { get; set; }
        public int Quantity { get; set; }
        public string imageUrl { get; set; }

    }
}
