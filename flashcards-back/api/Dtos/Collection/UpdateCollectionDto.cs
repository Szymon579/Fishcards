using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Collection
{
    public class UpdateCollectionDto
    {
        public int Id {get; set; }  
        public string Title {get; set; } = string.Empty; 
    }
}