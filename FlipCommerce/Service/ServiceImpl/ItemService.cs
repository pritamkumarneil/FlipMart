using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;

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

        public ItemResponseDto AddItemToCart(ItemRequestDto itemRequestDto)
        {
            // first find the customer with given customer mail/ or id
            // then find the cart associated to that customer
            // then find the product by productId
            // then check its quantity->// return according to quantity available
            // add this item  inside list of items in product entity
            // add product in item
            // add item to cart 

            // finding customer and validating 
            if (flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("No cutomer exist");
            }
            string customerMail = itemRequestDto.CustomerMail;
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c => c.EmailId.Equals(customerMail))
                .Include(c => c.cart)
                .ThenInclude(c=>c.Items)
                .ThenInclude(i=>i.product)
                .FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException("No customer with given mail id exist");
            }

            //finding cart and validating
            Cart cart = customer.cart;

            // finding product and validating
            int requiredQuantity= itemRequestDto.RequiredQuantity;
           
            if (flipCommerceDbContext.Products == null)
            {
                throw new ProductNotFoundException("Products Not Available");
            }

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

            // first finding the product in list of items in cart
            Item item=null;
            // update the cart with total value
            int totalValue = cart.CartTotal;
            // if product in item already availabe and is in the cart also 
            // then simply increasing the count of product inside that item 
            // and also updating the new cart value .. we are not adding new item for same product
            foreach (Item item1 in cart.Items)
            {
                if (item1.product.Equals(product))
                {
                    int totalRequiredQuantity = item1.RequiredQuantity + requiredQuantity;
                    if (totalRequiredQuantity > availableQuantity)
                    {
                        throw new ProductQantityLesserException(totalRequiredQuantity+ product.Name+ " not available in stock ");
                    }
                    item1.RequiredQuantity = totalRequiredQuantity;
                    cart.CartTotal += (requiredQuantity * product.Price);
                    item = item1;
                    break;
                }
            }
            // if product inside item found then update the cart with existing item only
            if (item != null)
            {
                flipCommerceDbContext.Carts.Update(cart);
                flipCommerceDbContext.SaveChanges();
                return ItemTranformer.ItemToItemResponseDto(item);
            }
            // if product not found in the list of items in cart then will create new item and add to 
            // the list of items in cart
            item = ItemTranformer.ItemRequestDtoToItem(itemRequestDto);

            item.RequiredQuantity = requiredQuantity;
            cart.CartTotal = totalValue + requiredQuantity * product.Price;
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
            catch(Exception e)
            {
                throw new Exception("Coudnot save changes");
            }
           
            return ItemTranformer.ItemToItemResponseDto(item);
        }

        Item IItemService.AddItem(ItemRequestDto itemRequestDto)
        {
            //customer Validation
            if (flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("No cutomer exist");
            }
            string customerMail = itemRequestDto.CustomerMail;
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c => c.EmailId.Equals(customerMail))
                .Include(c => c.cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.product)
                .FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException("No customer with given mail id exist");
            }

            //product validation
            int requiredQuantity = itemRequestDto.RequiredQuantity;

            if (flipCommerceDbContext.Products == null)
            {
                throw new ProductNotFoundException("Products Not Available");
            }

            Product? product = flipCommerceDbContext.Products.Find(itemRequestDto.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException("Product with given Id Not found");
            }
            int availableQuantity = product.Quantity;

            if (requiredQuantity > availableQuantity)
            {
                throw new ProductQantityLesserException("product is not available in given quantity");
            }

            Item item = ItemTranformer.ItemRequestDtoToItem(itemRequestDto);

            item.RequiredQuantity = requiredQuantity;

            return item;

        }
    }
}
