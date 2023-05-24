﻿using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlipCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CustomerResponseDto>> AddCustomer(CustomerRequestDto customerRequestDto)
        {
            try
            {
                CustomerResponseDto customer = customerService.AddCustomer(customerRequestDto);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("get/all")]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAllCustomers()
        {
            try
            {
                List<CustomerResponseDto> customers = customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
