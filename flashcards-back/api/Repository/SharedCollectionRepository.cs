using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SharedCollectionRepository : ISharedCollectionRepository
    {
        private readonly ApplicationDBContext _context;

        public SharedCollectionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Collection>?> GetShared(User user)
        {
            var shares = await _context.SharedCollections.Where(u => u.SharedWithUserId == user.Id).ToListAsync();

            var collectionsIds = shares.Select(s => s.CollectionId).ToList();

            var sharedCollections = await _context.Collections.Include(x => x.Cards).Where(c => collectionsIds.Contains(c.Id)).ToListAsync();

            return sharedCollections;
        }

        public async Task<SharedCollection?> Share(SharedCollection sharedCollection)
        {
            await _context.SharedCollections.AddAsync(sharedCollection);
            await _context.SaveChangesAsync();

            return sharedCollection;
        }
    }
}