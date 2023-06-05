namespace FlipCommerce.DTO.RequestDto
{
    public class OrderRequestDto
    {
        public string CustomerMail { get; set; }
        public int PoductId { get; set; }
        public int RequiredQuantity { get; set; }
        public string CardNo { get; set; }
        public int CVV { get; set; }
        public  AddressDto address { get; set; }
    }
}
