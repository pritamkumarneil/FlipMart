using FlipCommerce.DTO.RequestDto;
using FlipCommerce.Model;

namespace FlipCommerce.Transformer
{
    public class AddressTransformer
    {
        public static DeliveryAddress AddressRequestDtoToDeliveryAddress(AddressDto addressDto)
        {
            DeliveryAddress address = new DeliveryAddress();

            address.address1 = addressDto.address1;
            address.flat = addressDto.flat;
            address.Landmark = addressDto.Landmark;
            address.city = addressDto.city;
            address.Pincode = addressDto.Pincode;
            address.State = addressDto.State;
            address.MobNo = addressDto.MobNo;


            return address;
        }
        public static AddressDto DeliveryAddressToAddressDto(DeliveryAddress address)
        {
            AddressDto addressDto = new AddressDto();
            addressDto.address1 = address.address1;
            addressDto.flat = address.flat;
            addressDto.Landmark = address.Landmark;
            addressDto.city = address.city;
            addressDto.Pincode = address.Pincode;
            addressDto.State = address.State;
            addressDto.MobNo = address.MobNo;
            return addressDto;
        }
        
    }
}
