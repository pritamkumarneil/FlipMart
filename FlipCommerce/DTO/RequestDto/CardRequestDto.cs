using FlipCommerce.Enums;

namespace FlipCommerce.DTO.RequestDto
{
    public class CardRequestDto
    {
        public string CustomerEmailId { get; set; }
        public string CardNo { get; set; }
        public int CVV { get; set; }
        public CardType cardType { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
