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
        [HttpPost("add")]
        public async Task<ActionResult<ItemResponseDto>> AddItems(ItemRequestDto itemRequestDto)
        {
            try
            {
                ItemResponseDto item = itemService.AddItem(itemRequestDto);
                return Ok(item);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("add-to-cart")]
        public async Task<ActionResult<string>> AddItemToCart(int customerId,int itemId)
        {
            try
            {
                string response = itemService.AddToCart(customerId, itemId);
                return Ok(response);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
