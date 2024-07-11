using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ApplicationDBContext _context;

        public CollectionRepository(ApplicationDBContext context)
        {
            _context = context; 
        }

        public async Task<bool> CollectionExist(int id)
        {
            return await _context.Collections.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateCollection(Collection collection)
        {
            await _context.Collections.AddAsync(collection);

            return await SaveChanges();
        }

        public async Task<bool> DeleteCollection(int id)
        {
            var collectionModel = await _context.Collections.FirstOrDefaultAsync(x => x.Id == id);

            if(collectionModel == null)
                return false;

            _context.Collections.Remove(collectionModel);
                    
            return await SaveChanges();
        }

        public async Task<List<Collection>> GetAllCollections()
        {
            return await _context.Collections.ToListAsync();
        }

        public async Task<Collection?> GetCollectionById(int id)
        {
            return await _context.Collections.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Collection>> GetUserCollections(User user)
        {
            return await _context.Collections.Where(u => u.UserId == user.Id)
                .Select(collection => new Collection
                {
                    Id = collection.Id,
                    UserId = collection.UserId,
                    Title = collection.Title,
                    Cards = collection.Cards
                }).ToListAsync();
        }

        public async Task<bool> ShareCollection(int id, User user)
        {
            var collectionToShare = await _context.Collections.Include(c => c.Cards).FirstOrDefaultAsync(c => c.Id == id);

            if (collectionToShare == null)
                return false;

            var newCollection = new Collection
            {
                UserId = user.Id,
                User = user,
                Title = collectionToShare.Title,
                Cards = collectionToShare.Cards.Select(card => new Card
                {
                    FrontText = card.FrontText,
                    BackText = card.BackText
                }).ToList()
            };

            await _context.Collections.AddAsync(newCollection);

            return await SaveChanges();
        }

        public async Task<bool> UpdateCollection(int id, Collection collection)
        {
            var existingCollection = await _context.Collections.FindAsync(id);

            if(existingCollection == null)
                return false;

            existingCollection.Title = collection.Title;

            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}