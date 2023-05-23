using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Model;

namespace FlipCommerce.Transformer
{
    public class CardTransformer
    {
        public static Card CardRequestDtoToCard(CardRequestDto cardRequestDto)
        {
            Card card = new();
            card.CardNo= cardRequestDto.CardNo;
            card.cvv = cardRequestDto.CVV;
            card.cardType = cardRequestDto.cardType;
            card.ValidTill = cardRequestDto.ValidTill;
            return card;
        }
        public static CardResponseDto CardToCardResponseDto(Card card)
        {
            CardResponseDto cardResponseDto = new();

            cardResponseDto.CardNo=card.CardNo;
            cardResponseDto.CustomerName = card.custmer == null ? "" : card.custmer.Name;
            cardResponseDto.CardType = card.cardType.ToString();
            cardResponseDto.ValidTill = card.ValidTill;
            return cardResponseDto;
        }
    }
}
