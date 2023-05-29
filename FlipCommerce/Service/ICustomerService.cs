using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;

namespace FlipCommerce.Service
{
    public interface ICustomerService
    {
        public CustomerResponseDto AddCustomer(CustomerRequestDto customerRequestDto);
        public CustomerResponseDto GetCustomerByEmail(string email);
        public List<CustomerResponseDto> GetAllCustomers();
        public CartResponseDto GetCart(string customerMail);
    }
}
