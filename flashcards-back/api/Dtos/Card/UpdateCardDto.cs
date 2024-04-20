using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Card
{
    public class UpdateCardDto
    {
        public string FrontText {get; set; } = string.Empty;
        public string BackText {get; set; } = string.Empty;
    }
}