using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class SharedCollection
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; } = null!;
        public string SharedWithUserId { get; set; } = string.Empty;
        public User SharedWithUser { get; set; } = null!;
        public bool CanEdit { get; set; } = false;

    }
}