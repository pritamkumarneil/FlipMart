using FlipCommerce.Repository;

namespace FlipCommerce.Service.ServiceImpl
{
    public class CartService:ICartService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public CartService(FlipCommerceDbContext flipCommerceDbContext)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
        }
    }
}
