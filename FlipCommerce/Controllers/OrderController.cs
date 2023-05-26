using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlipCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("checkout/cart")]
        public async Task<ActionResult<OrderResponseDto>> CheckoutCart(CartCheckoutDto cartCheckoutDto)
        {
            try
            {
                OrderResponseDto order = orderService.CheckoutCart(cartCheckoutDto);
                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("make/order")]
        public async Task<ActionResult<OrderResponseDto>> MakeOrder(OrderRequestDto orderRequestDto)
        {
            try
            {
                OrderResponseDto order = orderService.MakeOrder(orderRequestDto);
                return Ok(order);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // check order status 
        // cancel order 

    }
}
