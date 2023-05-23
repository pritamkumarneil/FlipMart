using FlipCommerce.Repository;

namespace FlipCommerce.Service.ServiceImpl
{
    public class OrderService:IOrderService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public OrderService(FlipCommerceDbContext flipCommerceDbContext)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
        }

    }
}
