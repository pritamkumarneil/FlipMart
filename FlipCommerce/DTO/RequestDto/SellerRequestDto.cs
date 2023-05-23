using FlipCommerce.Enums;

namespace FlipCommerce.DTO.RequestDto
{
    public class SellerRequestDto
    {
        public string Name { get; set; }
        public string MobNo { get; set; }
        public string EmailId { get; set; }
        public int Age { get; set; }
        public Gender gender{ get; set; }
    }
}
