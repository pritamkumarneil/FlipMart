using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlipCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService itemService;
        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }
        [HttpPost("add/to-cart")]
        public async Task<ActionResult<ItemResponseDto>> AddItemToCart(ItemRequestDto itemRequestDto)
        {
            try
            {
                ItemResponseDto item = itemService.AddItemToCart(itemRequestDto);
                return Ok(item);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
    }
}
