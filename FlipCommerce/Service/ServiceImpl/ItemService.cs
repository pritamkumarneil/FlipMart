using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace FlipCommerce.Service.ServiceImpl
{
    public class ItemService:IItemService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public ItemService(FlipCommerceDbContext flipCommerceDbContext)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
        }

        ItemResponseDto IItemService.AddItem(ItemRequestDto itemRequestDto)
        {
            // first find the product by productId
            // then check its quantity->// return according to quantity available
            // add this item to inside list of items in product entity
            // add product in item
            if (flipCommerceDbContext.Products == null)
            {
                throw new ProductNotFoundException("Products Not Available");
            }
            int requiredQuantity= itemRequestDto.RequiredQuantity;

            Product? product = flipCommerceDbContext.Products.Find(itemRequestDto.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException("Product with given Id Not found");
            }
            int availableQuantity=product.Quantity;

            if (requiredQuantity > availableQuantity)
            {
                throw new ProductQantityLesserException("product is not available in given quantity");
            }

            Item item = ItemTranformer.ItemRequestDtoToItem(itemRequestDto);

            // make relation between item and product
            item.product = product;
            product.Items.Add(item);
            // need to add cart also as cart is parent to this entity 
            // otherwise it will throw some error -configure your entity type accordingly
            // it says we cant update a child entity without parent entity
            try
            {
                flipCommerceDbContext.Products.Update(product);
            }
            catch(Exception e) 
            {
                throw new Exception(" Couldnot update");
            }
            try
            {
                flipCommerceDbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception("Coudnot save changes");
            }
           
            return ItemTranformer.ItemToItemResponseDto(item);
        }

        string IItemService.AddToCart(int customerId, int itemId)
        {
            if (flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("No cusotomer Available");
            }
            Customer? customer = flipCommerceDbContext.Customers.Where(c => c.Id == customerId).Include(c => c.cart).FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer with given id is not available");
            }
            Cart? cart = customer.cart;
            if (cart == null)
            {
                throw new CartNotAttachedException("No cart Attached To the Given Customer");
            }
            Item? item = flipCommerceDbContext.Items.Where(i => i.Id == itemId).Include(i => i.product).FirstOrDefault();
            if (item == null)
            {
                throw new ItemNotFoundException("Item with given item id doesn't exist");
            }
            // calculate the total amount of the cart
            int totalAmount = cart.CartTotal;
            int productPrice = item.product.Price;
            int requiredQuantity = item.RequiredQuantity;

            totalAmount += productPrice * requiredQuantity;

            cart.CartTotal = totalAmount;

            // add relation between cart and items
            cart.Items.Add(item);
            item.cart = cart;

            flipCommerceDbContext.Customers.Update(customer);
            flipCommerceDbContext.SaveChanges();

            return requiredQuantity + " " + item.product.Name + " successfully Added to cart of " + customer.Name + ".";
        }
    }
}
