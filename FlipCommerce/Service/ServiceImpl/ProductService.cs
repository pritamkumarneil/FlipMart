﻿using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;

namespace FlipCommerce.Service.ServiceImpl
{
    public class ProductService:IProductService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public ProductService(FlipCommerceDbContext flipCommerceDbContext)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
        }

        ProductResponseDto IProductService.AddProduct(ProductRequestDto productRequestDto)
        {
            string sellerMail = productRequestDto.SellerMail;
            if (flipCommerceDbContext.Sellers == null)
            {
                throw new SellerNotFoundException("No Seller available");
            }
            Seller? seller = flipCommerceDbContext.Sellers.Where(s => s.EmailId.Equals(sellerMail)).FirstOrDefault();
            if (seller == null)
            {
                throw new SellerNotFoundException("Seller with mail " + sellerMail + " doesn't exist.");
            }
            Product product=ProductTransfomer.ProductRequestDtoToProduct(productRequestDto);

            // add seller to product and prouct to seller
            product.seller = seller;
            seller.Products.Add(product);

            flipCommerceDbContext.Update(seller);
            flipCommerceDbContext.SaveChanges();

            return ProductTransfomer.ProductToProductResponseDto(product);
        }
    }
}
