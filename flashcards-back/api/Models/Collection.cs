using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User user { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}