using FlipCommerce.DTO.RequestDto;
using FlipCommerce.DTO.ResponseDto;
using FlipCommerce.Exceptions;
using FlipCommerce.Model;
using FlipCommerce.Repository;
using FlipCommerce.Transformer;
using Microsoft.EntityFrameworkCore;

namespace FlipCommerce.Service.ServiceImpl
{
    public class CardService:ICardService
    {
        private readonly FlipCommerceDbContext flipCommerceDbContext;
        public CardService(FlipCommerceDbContext flipCommerceDbContext)
        {
            this.flipCommerceDbContext = flipCommerceDbContext;
        }

        CardResponseDto ICardService.AddCard(CardRequestDto cardRequestDto)
        {
            // first find the customer // to verify if customer exist with given mail or not
            string customerEmailId=cardRequestDto.CustomerEmailId;

            if (flipCommerceDbContext.Customers == null)
            {
                throw new CustomerNotFoundException("No Customer Available");
            }
            Customer? customer = flipCommerceDbContext.Customers.Where(c => c.EmailId.Equals(customerEmailId)).FirstOrDefault();
            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer with given EmailId doesn't exist");
            }

            Card card = CardTransformer.CardRequestDtoToCard(cardRequestDto);

            // now add card and customer relation
            card.custmer = customer;
            customer.Cards.Add(card);

            flipCommerceDbContext.Customers.Update(customer);
            flipCommerceDbContext.SaveChanges();

            return CardTransformer.CardToCardResponseDto(card);

        }
        public List<CardResponseDto> GetAllCardByCustomerMail(string customerMail)
        {
            if (flipCommerceDbContext.Cards == null)
            {
                throw new CardNotFoundException("No card Available");
            }
            Customer? customer = flipCommerceDbContext.Customers.Where(c => c.EmailId.Equals(customerMail)).Include(c=>c.Cards).FirstOrDefault();

            if (customer == null)
            {
                throw new CustomerNotFoundException("customer with given emailId not found");
            }
            List<CardResponseDto> cards = new List<CardResponseDto>();
            foreach(Card card in customer.Cards.ToList())
            {
                cards.Add(CardTransformer.CardToCardResponseDto(card));
            }
            return cards;

        }

    }
}
