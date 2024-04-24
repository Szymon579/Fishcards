using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ShareCollection
{
    public class ShareCollecitonDto
    {
        public int CollectionId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
    }
}