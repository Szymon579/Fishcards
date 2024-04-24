using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class User : IdentityUser
    {
        public List<Collection> Collections { get; set; } = new List<Collection>();
        public List<SharedCollection> SharedCollections = new List<SharedCollection>();
    }
}