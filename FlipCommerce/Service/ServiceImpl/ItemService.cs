using FlipCommerce.Repository;

namespace FlipCommerce.Service.ServiceImpl
{
    public class ItemService:IItemService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public ItemService(FlipCommerceDbContext flipCommerceDbContext)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
        }
    }
}
