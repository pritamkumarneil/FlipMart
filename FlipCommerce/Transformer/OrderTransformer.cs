using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Model;

namespace FlipCommerce.Transformer
{
    public class OrderTransformer
    {
        public static Order OrderRequestDtoToOrder(OrderRequestDto orderRequestDto)
        {
            Order order = new();

            order.OrderDate = DateTime.Now;
            order.OrderNo=Guid.NewGuid().ToString();
            
            return order;
        }
        public static OrderResponseDto OrderToOrderResponseDto(Order order)
        {
            OrderResponseDto orderResponseDto = new OrderResponseDto();

            orderResponseDto.OrderDate = order.OrderDate;
            orderResponseDto.OrderNo = order.OrderNo;
            orderResponseDto.OrderStatus = order.Status.ToString() ;
            int n=order.CardUsed.Length;
            orderResponseDto.CardUsed = "XXXX XXXX XXXX "+order.CardUsed.Substring(n-5,4);
            orderResponseDto.TotalAmount = order.OrderValue;

            return orderResponseDto;
        }
    }
}
