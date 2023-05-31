using FlipCommerce.Enums;

namespace FlipCommerce.DTO.ResponseDto
{
    public class ProductResponseDto
    {
        public int productId { get; set; }
        public string ProductName { get; set; }
        public string SellerName { get; set; }
        public string Status { get; set; }
        public int ProductPrice { get; set; }
        public string category { get; set; }
        public string Message { get; set; }
        public int availableQuanity { get; set; }
        public List<string> imageUrls { get; set; }
    }
}
