using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Enums;
using FlipCommerce.Model;

namespace FlipCommerce.Transformer
{
    public class SellerTransformer
    {
        public static Seller SellerRequestDtotoSeller(SellerRequestDto sellerRequestDto)
        {
            Seller seller = new Seller();
            seller.Name=sellerRequestDto.Name;
            seller.Age = sellerRequestDto.Age;
            seller.MobNo=sellerRequestDto.MobNo;
            seller.EmailId=sellerRequestDto.EmailId;
            seller.gender = sellerRequestDto.gender;
            Console.WriteLine(seller.gender + " RequestDto Gender console Print");

            return seller;
        }
        public static SellerResponseDto SellerToSellerResponseDto(Seller seller)
        {
            SellerResponseDto sellerResponseDto = new();
            sellerResponseDto.Name = seller.Name;
            sellerResponseDto.EmailId = seller.EmailId;
            sellerResponseDto.gender = seller.gender;
            return sellerResponseDto;
        }
    }
}
