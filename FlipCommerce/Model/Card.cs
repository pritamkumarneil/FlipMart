using FlipCommerce.Enums;

namespace FlipCommerce.Model
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNo { get; set; }
        public int cvv { get; set; }
        public CardType cardType { get; set; }
        public DateTime ValidTill { get; set; }

        // Navigational Properties
        public int CustomerId { get; set; }
        public Customer custmer { get; set; }
    }
}
