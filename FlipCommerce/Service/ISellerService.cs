using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using Microsoft.AspNetCore.Mvc;

namespace FlipCommerce.Service
{
    public interface ISellerService
    {
        public SellerResponseDto AddSeller(SellerRequestDto sellerRequestDto);
        public List<SellerResponseDto> GetSellers();
        public string GetSellersInString();
    }
}
