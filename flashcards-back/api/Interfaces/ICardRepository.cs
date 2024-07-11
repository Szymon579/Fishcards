using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICardRepository
    {
        Task<bool> CardExist(int id);
        Task<Card?> GetCardById(int id);
        Task<List<Card>?> GetCardsByCollectionId(int collectionId);
        Task<bool> CreateCard(Card card);
        Task<bool> UpdateCard(int id, Card card);
        Task<bool> DeleteCard(int id);
        Task<bool> SaveChanges();
    }
}