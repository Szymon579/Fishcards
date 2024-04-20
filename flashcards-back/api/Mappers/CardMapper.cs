using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Card;
using api.Dtos.CardDTO;
using api.Models;

namespace api.Mappers
{
    public static class CardMapper
    {
        public static CardDto ToCardDto(this Card card) 
        {
            return new CardDto
            {
                Id = card.Id,
                FrontText = card.FrontText,
                BackText = card.BackText
            };
        } 

        public static Card ToCardFromCreateDto(this CreateCardDto cardDto)
        {
            return new Card
            {
                FrontText = cardDto.FrontText,
                BackText = cardDto.BackText,
                CollectionId = cardDto.CollectionId
            };
        }

        public static Card ToCardFromUpdateDto(this UpdateCardDto cardDto)
        {
            return new Card
            {
                FrontText = cardDto.FrontText,
                BackText = cardDto.BackText,
            };
        }
    }
}