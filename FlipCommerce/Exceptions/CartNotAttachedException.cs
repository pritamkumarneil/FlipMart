namespace FlipCommerce.Exceptions
{
    public class CartNotAttachedException:Exception
    {
        public CartNotAttachedException(string message) : base(message) { }
    }
}
