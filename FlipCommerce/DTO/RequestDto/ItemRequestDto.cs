namespace FlipCommerce.DTO.RequestDto
{
    public class ItemRequestDto
    {
        public string CustomerMail {get; set; }
        public int ProductId { get; set; }
        public int RequiredQuantity { get; set; }
    }
}
