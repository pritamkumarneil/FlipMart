using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Model;

namespace FlipCommerce.Transformer
{
    public class ItemTranformer
    {
        public static Item ItemRequestDtoToItem(ItemRequestDto itemRequestDto)
        {
            Item item = new Item();

            item.RequiredQuantity= itemRequestDto.RequiredQuantity;

            return item;
        }
        public static ItemResponseDto ItemToItemResponseDto(Item item)
        {
            ItemResponseDto itemResponseDto = new ItemResponseDto();

            itemResponseDto.Qantity = item.RequiredQuantity;
            itemResponseDto.ProductName = item.product == null ? "Not Found!" : item.product.Name;
            
            return itemResponseDto;
        }
    }
}
