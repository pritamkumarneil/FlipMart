﻿namespace FlipCommerce.DTO.ResponseDto
{
    public class ItemResponseDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Message { get; set; }
        public ProductResponseDto product {get;set;}
    }
}
