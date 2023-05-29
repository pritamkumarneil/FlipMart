namespace FlipCommerce.DTO.ResponseDto
{
    public class CartResponseDto
    {
        public int CartTotal { get; set; }
        public List<ItemResponseDto> Items { get; set; }

    }
}
