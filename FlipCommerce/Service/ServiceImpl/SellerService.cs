using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlipCommerce.Service.ServiceImpl
{
    public class SellerService : ISellerService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public SellerService(FlipCommerceDbContext flipCommerceDbContext) 
        {
            this.flipCommerceDbContext=flipCommerceDbContext;
        }  
        public SellerResponseDto AddSeller(SellerRequestDto sellerRequestDto)
        {
            Seller seller=SellerTransformer.SellerRequestDtotoSeller(sellerRequestDto);
            flipCommerceDbContext.Sellers.Add(seller);
            flipCommerceDbContext.SaveChanges();
            return SellerTransformer.SellerToSellerResponseDto(seller);
            
        }

         List<SellerResponseDto> ISellerService.GetSellers()
        {
            if (flipCommerceDbContext.Sellers == null)
            {
                throw new SellerNotFoundException("No Seller Exists");
            }
            List<Seller> sellers;
            try
            {
                sellers = flipCommerceDbContext.Sellers.ToList();   

            }catch(Exception e)
            {
                throw new Exception("couldn't access Enum from DATABASE");
            }

            List<SellerResponseDto> sellerResponseDtos = new();
            
            foreach(Seller seller in sellers)
            {
                try
                {
                    sellerResponseDtos.Add(SellerTransformer.SellerToSellerResponseDto(seller));

                }
                catch (Exception e)
                {
                    throw new Exception("Coudn't convert String To Enum");
                }
            }
            return sellerResponseDtos;
        }

        string ISellerService.GetSellersInString()
        {
            if (flipCommerceDbContext.Sellers == null)
            {
                throw new SellerNotFoundException("No Seller Exists");
            }
            List<Seller> sellers;
            try
            {
                sellers = flipCommerceDbContext.Sellers.ToList();

            }
            catch (Exception e)
            {
                throw new Exception("couldn't access Enum from DATABASE");
            }
            string ans = "";
            foreach(Seller seller in sellers)
            {
                ans += seller.ToString()+"\n";

                Console.WriteLine(seller.gender+" Prited from Console");
                SellerResponseDto sellerResponseDto = SellerTransformer.SellerToSellerResponseDto(seller);
                //Console.WriteLine(sellerResponseDto.gender + "from Response dto after conversion");
            }
            return ans;
        }
    }
}
