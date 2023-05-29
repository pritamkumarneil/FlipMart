using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface IOrderService
    {
        public OrderResponseDto CheckoutCart(CartCheckoutDto cartCheckoutDto);
        public OrderResponseDto MakeOrder(OrderRequestDto orderRequestDto);
        public List<OrderResponseDto> GetOrders(string customerMail);
        string CheckStatus(string orderNo);
        public OrderResponseDto CancelOrder(string orderNo);
    }
}
