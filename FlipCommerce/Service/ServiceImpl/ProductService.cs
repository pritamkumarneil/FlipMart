using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;
using Microsoft.EntityFrameworkCore;

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

            // add product image enitity
            ProductImage image = new ProductImage();
            image.ImageUrl = productRequestDto.imageUrl;
            // making relation between product and productImage
            product.ProductImages.Add(image);
            image.product = product;


            // add seller to product and prouct to seller
            product.seller = seller;
            seller.Products.Add(product);

            flipCommerceDbContext.Update(seller);
            flipCommerceDbContext.SaveChanges();

            return ProductTransfomer.ProductToProductResponseDto(product);
        }
        public List<ProductResponseDto> GetAllProducts()
        {
            if (flipCommerceDbContext.Products == null)
            {
                throw new ProductNotFoundException("No Products Available");
            }
            List<Product> products =  flipCommerceDbContext.Products.Include(p=>p.seller).Include(p=>p.ProductImages).ToList();
            List<ProductResponseDto> ans = new List<ProductResponseDto>();
            foreach (Product product in products)
            {
                ans.Add(ProductTransfomer.ProductToProductResponseDto(product));
            }
            return ans;
        }
    }
}
