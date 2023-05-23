using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlipCommerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<SellerResponseDto>> AddStudent(SellerRequestDto sellerRequestDto)
        {
            try
            {
                SellerResponseDto seller = _sellerService.AddSeller(sellerRequestDto);
                return Ok(seller);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("get/all")]
        public async Task<ActionResult<IEnumerable<SellerResponseDto>>> GetAllSellers()
        {
            try
            {
                List<SellerResponseDto> sellers = _sellerService.GetSellers();
                return Ok(sellers);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("get/all/asString")]
        public async Task<ActionResult<string>> GetAllSellersInString()
        {
            try
            {
               string sellers =  _sellerService.GetSellersInString();
                return Ok(sellers);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
