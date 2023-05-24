using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface ICardService
    {
        public CardResponseDto AddCard(CardRequestDto cardRequestDto);
        public List<CardResponseDto> GetAllCardByCustomerMail(string customerMail);

    }
}
