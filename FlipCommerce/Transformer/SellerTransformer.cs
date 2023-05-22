using FlipCommerce.DTO.RequestDto;
using FlipCommerce.Model;

namespace FlipCommerce.Transformer
{
    public class SellerTransformer
    {
        public static Seller SellerRequestDtotoSeller(SellerRequestDto sellerRequestDto)
        {
            Seller seller = new Seller();

            return seller;
        }
    }
}
