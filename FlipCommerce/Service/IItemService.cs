using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Model;

namespace FlipCommerce.Service
{
    public interface IItemService
    {
       
        public ItemResponseDto AddItemToCart(ItemRequestDto itemRequestDto);
        public Item AddItem(ItemRequestDto itemRequestDto);
    }
}
