using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICollectionRepository
    {
        Task<List<Collection>> GetAllCollections();
        Task<Collection?> GetCollectionById(int id);
        Task<List<Collection>> GetUserCollections(User user);
        Task<bool> ShareCollection(int id, User user);
        Task<bool> CreateCollection(Collection collection);
        Task<bool> UpdateCollection(int id, Collection collection);
        Task<bool> DeleteCollection(int id);
        Task<bool> CollectionExist(int id);
        Task<bool> SaveChanges();
    }
}