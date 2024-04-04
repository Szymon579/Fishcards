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
        Task<Collection> CreateAsync(Collection collection);
        Task<Collection?> DeleteAsync(int id);
        Task<bool> CollectionExist(int id);
    }
}