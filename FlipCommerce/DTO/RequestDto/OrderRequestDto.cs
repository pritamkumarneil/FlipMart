namespace FlipCommerce.DTO.RequestDto
{
    public class OrderRequestDto
    {
        public string CustomerMail { get; set; }
        public string CardNo { get; set; }
        public int CVV { get; set; }
    }
}
