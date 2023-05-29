using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Model;

namespace FlipCommerce.Transformer
{
    public class CartTransformer
    {
        public static  CartResponseDto CartToCartResponseDto(Cart cart)
        {
            CartResponseDto cartResponseDto = new CartResponseDto();

            cartResponseDto.CartTotal = cart.CartTotal;
            cartResponseDto.Items = new List<ItemResponseDto>();
            foreach(Item item in cart.Items)
            {
                cartResponseDto.Items.Add(ItemTranformer.ItemToItemResponseDto(item));
            }
            return cartResponseDto;
        }
    }
}
