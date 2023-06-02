namespace FlipCommerce.DTO.RequestDto
{
    public class CartCheckoutDto
    {
        public string CustomerMail { get; set; }
        public string CardNo { get; set; }
        public int CVV { get; set; }
        public AddressDto address { get; set;  }
    }
}
