using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDBContext _context;

        public CardRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CardExist(int id)
        {
            return await _context.Cards.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateCard(Card cardModel)
        {
            await _context.Cards.AddAsync(cardModel);

            return await SaveChanges();
        }

        public async Task<bool> DeleteCard(int id)
        {
            var cardModel = _context.Cards.FirstOrDefault(c => c.Id == id);
            
            if(cardModel == null)
                return false;

            _context.Cards.Remove(cardModel);

            return await SaveChanges();
        }

        public async Task<List<Card>?> GetCardsByCollectionId(int collectionId)
        {
            return await _context.Cards.Where(c => c.CollectionId == collectionId).ToListAsync();
        }

        public async Task<Card?> GetCardById(int id)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateCard(int id, Card card)
        {
            var existingCard = await _context.Cards.FindAsync(id);

            if(existingCard == null)
                return false;
                
            existingCard.FrontText = card.FrontText;
            existingCard.BackText = card.BackText;

            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}