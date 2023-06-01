using FlipCommerce.DTO.RequestDto;

namespace FlipCommerce.DTO.ResponseDto
{
    public class OrderResponseDto
    {
        public string OrderNo { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string CardUsed { get; set; }// only last four digit should be visible
        public AddressDto address { get; set; }
    }
}
