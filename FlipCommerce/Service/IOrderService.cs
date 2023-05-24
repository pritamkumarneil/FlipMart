using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface IOrderService
    {
        public OrderResponseDto MakeOrder(OrderRequestDto orderRequestDto);
    }
}
