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
            //product.ProductImages.Add();
            return product;
        } 
        public static ProductResponseDto ProductToProductResponseDto(Product product)
        {
            ProductResponseDto productResponseDto = new();
            productResponseDto.productId = product.Id;
            productResponseDto.ProductName = product.Name;
            productResponseDto.SellerName = product.seller == null ? "" : product.seller.Name;
            productResponseDto.Status = product.productStatus.ToString();
            productResponseDto.ProductPrice = product.Price;
            productResponseDto.availableQuanity = product.Quantity;
            productResponseDto.category = product.category.ToString();
            productResponseDto.imageUrls=new List<string> ();
            foreach(ProductImage image in product.ProductImages)
            {
                productResponseDto.imageUrls.Add(image.ImageUrl);
            }
            return productResponseDto;
        }
    }
}
