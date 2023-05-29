using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;
using Microsoft.EntityFrameworkCore;

namespace FlipCommerce.Service.ServiceImpl
{
    public class CustomerService:ICustomerService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        private readonly ICartService cartService;
        public CustomerService(FlipCommerceDbContext context,ICartService cartService1)
        {
            cartService = cartService1;
            flipCommerceDbContext = context;
        }

        CustomerResponseDto ICustomerService.AddCustomer(CustomerRequestDto customerRequestDto)
        {
            // in order to add customer .. we have to add Cart also simultaneously
            // for that we need cart service dependency here 
            Cart cart = new Cart();
            cart.CartTotal = 0;
            Customer customer = CustomerTransformer.CustomerRequestDtoToCustomer(customerRequestDto);

            // assigning the related properties
            customer.cart = cart;
            cart.customer = customer;
            // add the parent and child will automatically be saved
            flipCommerceDbContext.Customers.Add(customer);
            flipCommerceDbContext.SaveChanges();
            return CustomerTransformer.CustomerToCustomerResponseDto(customer);
        }

        List<CustomerResponseDto> ICustomerService.GetAllCustomers()
        {
            if(flipCommerceDbContext.Customers== null)
            {
                throw new CustomerNotFoundException("No Customer Available");
            }
            List<Customer> customers = flipCommerceDbContext.Customers.ToList();
            List<CustomerResponseDto> ans = new();
            foreach(Customer customer in customers)
            {
                ans.Add(CustomerTransformer.CustomerToCustomerResponseDto(customer));
            }
            return ans;
        }

        CustomerResponseDto ICustomerService.GetCustomerByEmail(string email)
        {
            if(flipCommerceDbContext.Customers== null)
            {
                throw new CustomerNotFoundException("No Customer with given mail available");
            }
            
            Customer? customer = flipCommerceDbContext.Customers.Where(c=>c.EmailId.Equals(email)).FirstOrDefault();

            if (customer == null)
            {
                throw new CustomerNotFoundException("Costomer not found with emailId");
            }
            return CustomerTransformer.CustomerToCustomerResponseDto(customer);
        }
        public CartResponseDto GetCart(string customerMail)
        {
            if (flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("Customer Not Found");
            }
            Customer? customer = flipCommerceDbContext.Customers
                .Where(c => c.EmailId.Equals(customerMail))
                .Include(c => c.cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.product)
                .FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer with given mail doesn't Exist");
            }

            return CartTransformer.CartToCartResponseDto(customer.cart);
        }
    }
}
