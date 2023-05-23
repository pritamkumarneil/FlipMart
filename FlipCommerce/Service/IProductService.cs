using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface IProductService
    {
        public ProductResponseDto AddProduct(ProductRequestDto productRequestDto);

    }
}
