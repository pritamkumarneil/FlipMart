using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FlipCommerce.Transformer
{
    public class ProductTransfomer
    {
        public static Product ProductRequestDtoToProduct(ProductRequestDto productRequestDto)
        {
            Product product = new Product();
            product.Name = productRequestDto.ProductName;
            product.Price = productRequestDto.Price;
            product.Quantity = productRequestDto.Quantity;
            product.category = productRequestDto.category;
            return product;
        } 
        public static ProductResponseDto ProductToProductResponseDto(Product product)
        {
            ProductResponseDto productResponseDto = new();

            productResponseDto.ProductName = product.Name;
            productResponseDto.SellerName = product.seller == null ? "" : product.seller.Name;
            productResponseDto.Status = product.productStatus.ToString();
            productResponseDto.ProductPrice = product.Price;


            return productResponseDto;
        }
    }
}
