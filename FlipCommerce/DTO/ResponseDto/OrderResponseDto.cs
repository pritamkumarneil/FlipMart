using FlipCommerce.DTO.RequestDto;

namespace FlipCommerce.DTO.ResponseDto
{
    public class OrderResponseDto
    {
        public string OrderNo { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public int TotalAmount { get; set; }
        // only last four digit should be visible
        public string CardUsed { get; set; }
        public AddressDto address { get; set; }
    }
}
