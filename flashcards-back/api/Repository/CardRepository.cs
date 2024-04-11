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

        public async Task<Card> CreateAsync(Card cardModel)
        {
            await _context.Cards.AddAsync(cardModel);
            await _context.SaveChangesAsync();

            return cardModel;
        }

        public async Task<Card?> Delete(int id)
        {
            var cardModel = _context.Cards.FirstOrDefault(c => c.Id == id);
            
            if(cardModel == null)
            {
                return null;
            }

            _context.Cards.Remove(cardModel);
            await _context.SaveChangesAsync();

            return cardModel;
        }

        public async Task<List<Card>> GetByCollectionIdAsync(int collectionId)
        {
            return await _context.Cards.Where(c => c.CollectionId == collectionId).ToListAsync();
        }

        public async Task<Card?> GetByIdAsync(int id)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}