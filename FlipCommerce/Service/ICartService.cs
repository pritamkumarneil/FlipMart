using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface ICartService
    {
        public CartResponseDto AddItemToCart(ItemRequestDto itemRequestdto);
        public CartResponseDto GetCart(int customerId);
    }
}
