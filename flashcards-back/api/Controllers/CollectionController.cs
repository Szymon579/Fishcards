using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Card;
using api.Dtos.CardsCollection;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    
    [Route("api/collections")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepo;
        private readonly UserManager<User> _userManager;
        
        public CollectionController(ICollectionRepository collectionRepo, UserManager<User> userManager) 
        {
            _collectionRepo = collectionRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserCollections()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return BadRequest("User not found");
            }

            var userCollections = await _collectionRepo.GetUserCollections(user);
            return Ok(userCollections.Select(c => c.ToCollectionDto()));
        }
        
        [HttpGet("{collectionId}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int collectionId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
            {
                return BadRequest("User not found");
            }

            var collection = await _collectionRepo.GetByIdAsync(collectionId);
            
            if (collection == null)
            {
                return NotFound();
            }

            if(user.Id != collection.UserId)
            {
                return BadRequest("User not authorized for this collection");
            }

            return Ok(collection.ToCollectionWithCardsDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateByUser([FromBody] CreateCollectionDto collectionDto) 
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
            {
                return BadRequest("User not found");
            }

            var collection = new Collection
            {
                UserId = user.Id,
                Title = collectionDto.Title
            };

            await _collectionRepo.CreateByUser(collection);

            if(collection == null)
            {
                return StatusCode(500, "Could not create collection");
            }

            return Ok(collection.ToCollectionDto());
        }

        [HttpDelete]
        [Route("{collectionId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int collectionId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
            {
                return BadRequest("User not found");
            }
            
            var collection = await _collectionRepo.GetByIdAsync(collectionId);

            if (collection == null)
            {
                return BadRequest("Collection does not exist");
            }
            
            if(user.Id != collection.UserId)
            {
                return BadRequest("User not authorized for this collection");
            }

            await _collectionRepo.DeleteAsync(collectionId);

            return Ok(collection.ToCollectionDto());
        }

    }
}