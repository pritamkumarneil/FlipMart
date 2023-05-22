using FlipCommerce.Enums;

namespace FlipCommerce.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string EmailId { get; set; }
        public string MobNo { get; set; }
        public Gender gender { get; set; }

    }
}
