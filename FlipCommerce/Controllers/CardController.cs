using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlipCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService cardService;
        public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CardResponseDto>> AddCard(CardRequestDto cardRequestDto)
        {
            try
            {
                CardResponseDto card=cardService.AddCard(cardRequestDto);
                return Ok(card);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("customerEmail/{customerMail}/get")]
        public async Task<ActionResult<IEnumerable<CardResponseDto>>> GetCardsByCustomerMail(string customerMail)
        {
            try
            {
                List<CardResponseDto> cards = cardService.GetAllCardByCustomerMail(customerMail);
                return Ok(cards);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
