using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface IItemService
    {
        public ItemResponseDto AddItem(ItemRequestDto itemResponseDto);
        public string AddToCart(int customerId, int itemId);
    }
}
