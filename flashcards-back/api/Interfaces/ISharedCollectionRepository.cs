using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ISharedCollectionRepository
    {
        public Task<SharedCollection?> Share(SharedCollection sharedCollection);
        public Task<List<Collection>?> GetShared(User user);
        
    }
}