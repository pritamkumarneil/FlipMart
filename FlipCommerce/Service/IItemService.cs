using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface IItemService
    {
       
        public ItemResponseDto AddItemToCart(ItemRequestDto itemRequestDto);
    }
}
