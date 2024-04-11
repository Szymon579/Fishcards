using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int CollectionId { get; set;}
        public Collection Collection { get; set; } = null!;
        public string FrontText { get; set; } = string.Empty;
        public string BackText { get; set; } = string.Empty;

    }
}