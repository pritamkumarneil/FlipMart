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

        OrderResponseDto IOrderService.CheckoutCart(CartCheckoutDto cartCheckoutDto)
        {
            string customerMail = cartCheckoutDto.CustomerMail;
            int CVV = cartCheckoutDto.CVV;
            string cardNo = cartCheckoutDto.CardNo;
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

            Order order = OrderTransformer.CartCheckoutDtoToOrder(cartCheckoutDto);

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
            order.DeliveryDate= DateTime.Now.AddDays(4).Date;
            order.Status = Enums.OrderStatus.IN_PROGRESS;

            // removing all items from cart;
            cart.CartTotal = 0;
            foreach(Item item in itemsInCart)
            {
                itemsInCart.Remove(item);
            }
            // adding delivery Address to the customer and order 
            DeliveryAddress address = AddressTransformer.AddressRequestDtoToDeliveryAddress(cartCheckoutDto.address);
            address.Orders.Add(order);
            order.address = address;
            // making relation between customer and address
            customer.addresses.Add(address);
            address.customer = customer;
            // make relation between customer to order
            order.customer = customer;
            customer.Orders.Add(order);

            flipCommerceDbContext.Customers.Update(customer);
            flipCommerceDbContext.SaveChanges();
            return OrderTransformer.OrderToOrderResponseDto(order);

        }

        OrderResponseDto IOrderService.MakeOrder(OrderRequestDto orderRequestDto)
        {
            // first check if customer is valid or not 
            // then check if the card details are valid or not 
            // then check if product is valid or not 
            // again check if product is available or not 
            // if all goes well then create item 
            // relate this item to the product given 
            // add item to the list of items in order and make relation between them
            // make relation between customer and order
            // finaly save the order
            // return the order response dto 

            // Validating Customer Details
            string customerMail = orderRequestDto.CustomerMail;
            if (flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("No Customer Found");
            }
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c => c.EmailId.Equals(customerMail))
                .Include(c => c.Cards)
                .Include(c=>c.Orders)
                .ThenInclude(o=>o.Items)
                .ThenInclude(i=>i.product)
                .FirstOrDefault() ?? throw new CustomerNotFoundException("Customer with given id doens't exist");


            // validating Carddetails
            string cardNo = orderRequestDto.CardNo;
            int CVV = orderRequestDto.CVV;
            Card? card = flipCommerceDbContext.Cards.Where(c => c.CardNo.Equals(cardNo)).FirstOrDefault() ?? throw new CardNotFoundException("No card exist with given detail");
            ICollection<Card> allCard = customer.Cards;
            if (!allCard.Contains(card))
            {
                throw new CardNotFoundException("Card Not found!!!");
            }
            else if (card.cvv != CVV)
            {
                throw new WrongCVVException("Please Enter correct CVV");
            }

            // Validate Product 
            Product? product = flipCommerceDbContext.Products.Find(orderRequestDto.PoductId) ?? throw new ProductNotFoundException("Invalid Product Id");
            if (product.productStatus.Equals(Enums.ProductStatus.OUT_OF_STOCK))
            {
                throw new ProductNotFoundException("Product is Out Of Stock");
            }
            if (product.Quantity < orderRequestDto.RequiredQuantity)
            {
                throw new ProductQantityLesserException("Insufficient Quantity Availabe for you order");
            }
            product.Quantity-=orderRequestDto.RequiredQuantity;
            if (product.Quantity == 0)
            {
                product.productStatus = Enums.ProductStatus.OUT_OF_STOCK;
            }
            // create Item 
            Item item = new Item();
            item.RequiredQuantity = orderRequestDto.RequiredQuantity;
            item.itemCost = (item.RequiredQuantity * product.Price * (100 - product.discount) + 50) / 100;
            
            // establishing relationship between item and product
            item.product = product;
            product.Items.Add(item);

            Order order = OrderTransformer.OrderRequestDtoToOrder(orderRequestDto);

            //order.OrderValue += (item.RequiredQuantity * (1 - (product.discount / 100)) * product.Price);
            order.OrderValue += item.itemCost;
            

            // establishing relation between order and items
            order.Items.Add(item);
            item.order=order;

            DeliveryAddress address = AddressTransformer.AddressRequestDtoToDeliveryAddress(orderRequestDto.address);
            address.Orders.Add(order);
            order.address = address;
            // making relation between customer and address
            customer.addresses.Add(address);
            address.customer = customer;

            // establishing relation between order and customer 
            order.customer = customer;
            customer.Orders.Add(order);
            flipCommerceDbContext.Customers.Update(customer);
            flipCommerceDbContext.SaveChanges();


            return OrderTransformer.OrderToOrderResponseDto(order);

            //throw new Exception("Function MakeOrder in Order Service is not implemented yet. Feature is comming soon!!");
        }
        public List<OrderResponseDto> GetOrders(string customerMail)
        {
            if(flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("Customer not found");
            }
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c=>c.EmailId.Equals(customerMail))
                .Include(c=>c.Orders)
                .ThenInclude(o=>o.address)
                .FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer not found");
            }
            List<OrderResponseDto> orders = new();

            foreach(Order order in customer.Orders)
            {
                orders.Add(OrderTransformer.OrderToOrderResponseDto(order));
            }
            return orders;
        }
        public string CheckStatus(string orderNo)
        {
            if (flipCommerceDbContext.Orders == null)
            {
                throw new OrderNotFoundException("No orders made yet");
            }
            Order? order=flipCommerceDbContext.Orders.Where(O=>O.OrderNo.Equals(orderNo)).FirstOrDefault() ?? throw new OrderNotFoundException("Wrong Ordre NO");
            return order.Status.ToString();
        }
        public OrderResponseDto CancelOrder(string orderNo)
        {
            if (flipCommerceDbContext.Orders == null)
            {
                throw new OrderNotFoundException("No orders made yet");
            }
            Order? order = flipCommerceDbContext.Orders
                .Where(O => O.OrderNo.Equals(orderNo))
                .Include(O=>O.Items)
                .ThenInclude(i=>i.product)
                .FirstOrDefault() ?? throw new OrderNotFoundException("Wrong Ordre NO");
            // check if order is delivered or alreadyCanceled
            if (order.Status.Equals(Enums.OrderStatus.CANCELLED)
                ||order.Status.Equals(Enums.OrderStatus.FAILED)
                ||order.Status.Equals(Enums.OrderStatus.DELIVERED))
            {
                throw new OrderNotFoundException("Cant Cancel This Order");
            }
            foreach(Item item in order.Items)
            {
                Product product = item.product;
                product.Quantity += item.RequiredQuantity;
            }
            order.Status = Enums.OrderStatus.CANCELLED;
            flipCommerceDbContext.SaveChanges();
            return OrderTransformer.OrderToOrderResponseDto(order);
        }
    }
}
