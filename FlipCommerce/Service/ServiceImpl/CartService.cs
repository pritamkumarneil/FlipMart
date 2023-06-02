using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;
using Microsoft.EntityFrameworkCore;

namespace FlipCommerce.Service.ServiceImpl
{
    public class CartService:ICartService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        private readonly IItemService itemService;
        public CartService(FlipCommerceDbContext flipCommerceDbContext,IItemService itemService)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
            this.itemService = itemService;
        }

        CartResponseDto ICartService.AddItemToCart(ItemRequestDto itemRequestDto)
        {
            Item item;
            try
            {
                // it also validate product and customer
                item = itemService.AddItem(itemRequestDto);
            }
            catch(Exception e)
            {
                throw e;
            }

            Product? product = flipCommerceDbContext.Products.Find(itemRequestDto.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException("Product Not found ");
            }
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c => c.EmailId
                .Equals(itemRequestDto.CustomerMail))
                .Include(c => c.cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.product)
                .FirstOrDefault();

            Cart cart = customer.cart;

            foreach(Item item1 in cart.Items.ToList()){
                if (item1.product.Id == product.Id)
                {
                    int totalRequiredQuantity = item1.RequiredQuantity + itemRequestDto.RequiredQuantity;
                    if (totalRequiredQuantity > product.Quantity)
                    {
                        throw new ProductQantityLesserException("Insufficient Product Available");
                    }
                    item1.RequiredQuantity = totalRequiredQuantity;
                    item1.itemCost += (itemRequestDto.RequiredQuantity * (1 - (product.discount / 100)) * product.Price);
                    cart.CartTotal += (itemRequestDto.RequiredQuantity * (1 - (product.discount / 100)) * product.Price);
                    return CartTransformer.CartToCartResponseDto(cart);
                }
            }
            //item.itemCost += (itemRequestDto.RequiredQuantity * (1 - (product.discount / 100)) * product.Price);
            // above line not needed because it is been taken care of in item service

            cart.CartTotal += (itemRequestDto.RequiredQuantity * (1 - (product.discount / 100)) * product.Price);
            // make relation between item and product
            item.product = product;
            product.Items.Add(item);

            item.cart = cart;
            cart.Items.Add(item);
            // need to add cart also as cart is parent to this entity 
            // otherwise it will throw some error -configure your entity type accordingly
            // it says we cant update a child entity without parent entity
            // try changing relation between enitity make foreign key nullabe by using '?' after datatype in entity


            try
            {
                flipCommerceDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Coudnot save changes");
            }

            return CartTransformer.CartToCartResponseDto(cart);

        }

        CartResponseDto ICartService.GetCart(int customerId)
        {
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c=>c.Id==customerId)
                .Include(c=>c.cart)
                .ThenInclude(c=>c.Items)
                .ThenInclude(i=>i.product)
                .FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException("Invalid Customer");
            }
            Cart cart = customer.cart;
            return CartTransformer.CartToCartResponseDto(cart);
        }
    }
}
