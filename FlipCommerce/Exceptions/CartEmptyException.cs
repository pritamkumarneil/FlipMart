﻿namespace FlipCommerce.Exceptions
{
    public class CartEmptyException:Exception
    {
        public CartEmptyException(string message) : base(message) { }   
    }
}
