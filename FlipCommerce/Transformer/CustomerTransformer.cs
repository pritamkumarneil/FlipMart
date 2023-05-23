using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FlipCommerce.Transformer
{
    public class CustomerTransformer
    {
        public static Customer CustomerRequestDtoToCustomer(CustomerRequestDto customerRequestDto)
        {
            Customer customer = new Customer();

            customer.Name=customerRequestDto.Name;
            customer.gender = customerRequestDto.gender;
            customer.MobNo=customerRequestDto.MobNo;
            customer.EmailId = customerRequestDto.EmailId;
            customer.Age = customerRequestDto.Age;
            return customer;
        }
       public static CustomerResponseDto CustomerToCustomerResponseDto(Customer customer)
        {
            CustomerResponseDto customerResponseDto = new CustomerResponseDto();
            customerResponseDto.Age = customer.Age;
            customerResponseDto.gender = customer.gender.ToString();
            customerResponseDto.Name = customer.Name;
            return customerResponseDto;
        }
    }
}
