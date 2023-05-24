namespace FlipCommerce.DTO.RequestDto
{
    public class OrderRequestDto
    {
        public int customerId { get; set; }
        public string CardNo { get; set; }
        public int CVV { get; set; }
    }
}
