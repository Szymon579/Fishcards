using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICollectionRepository
    {
        Task<List<Collection>> GetAllAsync();
        Task<Collection?> GetByIdAsync(int id);
        Task<List<Collection>> GetUserCollections(User user);
        Task<Collection> CreateAsync(Collection collection);
        Task<Collection> CreateByUser(Collection collection);
        Task<Collection?> UpdateAsync(int id, Collection collection);
        Task<Collection?> DeleteAsync(int id);
        Task<bool> CollectionExist(int id);
    }
}