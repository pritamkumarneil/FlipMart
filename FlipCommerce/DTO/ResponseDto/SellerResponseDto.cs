using FlipCommerce.Enums;

namespace FlipCommerce.DTO.ResponseDto
{
    public class SellerResponseDto
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public Gender gender { get; set; }
        public string Message { get; set; }

    }
}
