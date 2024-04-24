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
        Task<Card?> GetByIdAsync(int id);
        Task<List<Card>?> GetByCollectionIdAsync(int collectionId);
        Task<Card> CreateAsync(Card card);
        Task<Card?> UpdateAsync(int id, Card card);
        Task<Card?> Delete(int id);
    }
}