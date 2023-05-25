using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;
using Microsoft.EntityFrameworkCore;

namespace FlipCommerce.Service.ServiceImpl
{
    public class OrderService : IOrderService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public OrderService(FlipCommerceDbContext flipCommerceDbContext)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
        }

        OrderResponseDto IOrderService.MakeOrder(OrderRequestDto orderRequestDto)
        {
            string customerMail = orderRequestDto.CustomerMail;
            int CVV = orderRequestDto.CVV;
            string cardNo = orderRequestDto.CardNo;
            // first find customer with given id
            // then  associated to given customer find card 
            // validate all details of the card 
            // then get associated cart to the customer
            // get all items from the cart
            //  and check in loop for all items 
            // check if the required quantity of the items is available inside the product or not
            // if available then reduse the stock size of that particular product 
            // also if product quantity becomes zero then mark this product as OUT_OF_STOCK
            // and delete all the items associated to cart
            // also empty the cart and cart-TotalAmount
            // if all success then place the order
            // now add all those items in the order entity of listItems
            // make relation between customer to order , and order to items

            Order order = OrderTransformer.OrderRequestDtoToOrder(orderRequestDto);

            // finding the customer
            if (flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("No Customer Found");
            }
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c => c.EmailId.Equals(customerMail))
                .Include(c => c.Cards)
                .Include(c => c.cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.product)
                .FirstOrDefault();

            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer with given id doens't exist");
            }

            // finding the card and then authenting the details of card
            Card? card = flipCommerceDbContext.Cards.Where(c => c.CardNo.Equals(cardNo)).FirstOrDefault();
            if (card == null)
            {
                throw new CardNotFoundException("No card exist with given detail");
            }
            ICollection<Card> allCard = customer.Cards;
            if (!allCard.Contains(card))
            {
                throw new CardNotFoundException("Card Not found!!!");
            }
            else if (card.cvv != CVV)
            {
                throw new WrongCVVException("Please Enter correct CVV");
            }
            // getting associated cart to the customer
            Cart cart = customer.cart;
            // check if cart is empty or not 
            if (cart.CartTotal == 0 || cart.Items.Count == 0)
            {
                throw new CartEmptyException("Your cart is Empty");
            }

            int totalAmount = cart.CartTotal;
            //Console.WriteLine("Total Cart Amount: " + totalAmount);
            // get all the items in the cart
            ICollection<Item> itemsInCart = cart.Items;
            
            //now check if all the product is in stock according to items requiredQuantity

            foreach(Item item in itemsInCart)
            {
                Product product = item.product;
                if (product.productStatus == Enums.ProductStatus.OUT_OF_STOCK|| product.Quantity < item.RequiredQuantity)
                {
                    order.Status = Enums.OrderStatus.FAILED;
                    order.customer = customer;
                    customer.Orders.Add(order);
                    return OrderTransformer.OrderToOrderResponseDto(order);
                    //throw new ProductNotFoundException(product.Name + " is out of stock.");
                }
            }
            // if all are in stock .. then placing the order

            
            foreach(Item item in itemsInCart) 
            {
                // making relation between item and order also 
                order.Items.Add(item);
                item.order = order;
                // also reducing the stock by required quantity
                item.product.Quantity -= item.RequiredQuantity;
                if (item.product.Quantity == 0)
                {
                    item.product.productStatus = Enums.ProductStatus.OUT_OF_STOCK;
                }
            }
            order.OrderValue = totalAmount;
            order.CardUsed = card.CardNo;

            // removing all items from cart;
            cart.CartTotal = 0;
            foreach(Item item in itemsInCart)
            {
                itemsInCart.Remove(item);
            }

            // make relation between customer to order
            order.customer = customer;
            customer.Orders.Add(order);

            flipCommerceDbContext.Customers.Update(customer);
            flipCommerceDbContext.SaveChanges();


            return OrderTransformer.OrderToOrderResponseDto(order);

        }

    }
}
