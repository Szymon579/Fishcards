using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CardDTO;
using api.Models;


namespace api.Dtos.CardsCollection
{
    public class CollectionWithCardsDto
    {
        public int Id {get; set; }
        public string Title {get; set; } = string.Empty;
        public List<CardDto>? Cards { get; set; }
    }
}