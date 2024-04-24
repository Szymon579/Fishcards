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

        public async Task<Collection> CreateAsync(Collection collectionModel)
        {
            await _context.Collections.AddAsync(collectionModel);
            await _context.SaveChangesAsync();

            return collectionModel;
        }

        public async Task<Collection> CreateByUser(Collection collection)
        {
            await _context.Collections.AddAsync(collection);
            await _context.SaveChangesAsync();

            return collection;
        }

        public async Task<Collection?> DeleteAsync(int id)
        {
            var collectionModel = await _context.Collections.FirstOrDefaultAsync(x => x.Id == id);

            if(collectionModel == null)
            {
                return null;
            }

            _context.Collections.Remove(collectionModel);
            await _context.SaveChangesAsync();

            return collectionModel;
        }

        public async Task<List<Collection>> GetAllAsync()
        {
            return await _context.Collections.ToListAsync();
        }

        public async Task<Collection?> GetByIdAsync(int id)
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

        public async Task<Collection?> UpdateAsync(int id, Collection collection)
        {
            var existingCollection = await _context.Collections.FindAsync(id);

            if(existingCollection == null)
            {
                return null;
            }

            existingCollection.Title = collection.Title;

            await _context.SaveChangesAsync();

            return existingCollection;
        }
    }
}