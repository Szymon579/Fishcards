using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Card;
using api.Dtos.CardsCollection;
using api.Dtos.Collection;
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

        [HttpPost("share")]     
        [Authorize]
        public async Task<IActionResult> ShareCollection([FromBody] CollectionShareDto shareDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
                return BadRequest("User is null");

            var userToShare = await _userManager.FindByEmailAsync(shareDto.Email);
            
            if(userToShare == null)
                return BadRequest("User to share is null");

            if(!await _collectionRepo.ShareCollection(shareDto.Id, userToShare))
                return StatusCode(500, "Collection share failded");

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCollections()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
                return BadRequest("User is null");
            
            var userCollections = await _collectionRepo.GetUserCollections(user);
            
            return Ok(userCollections.Select(c => c.ToCollectionDto()));
        }
        
        [HttpGet("{collectionId}")]
        [Authorize]
        public async Task<IActionResult> GetCollectionById([FromRoute] int collectionId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
                return BadRequest("User not found");
            
            var collection = await _collectionRepo.GetCollectionById(collectionId);
            
            if (collection == null)
                return NotFound("Collection not found");
            

            if(user.Id != collection.UserId)
                return BadRequest("User not authorized for this collection");
            
            return Ok(collection.ToCollectionWithCardsDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionDto collectionDto) 
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
                return BadRequest("User is null");

            var collection = collectionDto.ToCollectionFromCreateDto(user.Id);

            if(!await _collectionRepo.CreateCollection(collection))
                return StatusCode(500, "Collection create failed");

            return Ok(collection.ToCollectionDto());
        }

        [HttpPut]
        [Route("{collectionId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCollection([FromRoute] int collectionId, [FromBody] UpdateCollectionDto collectionDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return BadRequest("User is null");

            var existingCollection = await _collectionRepo.GetCollectionById(collectionId);
            
            if(existingCollection == null)
                return BadRequest("Collection does not exist"); 
            
            if(user.Id != existingCollection.UserId)
                return BadRequest("Not authorized for this collection");

            if(!await _collectionRepo.UpdateCollection(collectionId, collectionDto.ToCollectionFromUpdateDto()))
                return StatusCode(500, "Collection update failed");

            return Ok("Collection updated");
        }

        [HttpDelete]
        [Route("{collectionId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCollection([FromRoute] int collectionId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
                return BadRequest("User not found");
            
            var collection = await _collectionRepo.GetCollectionById(collectionId);

            if (collection == null)
                return BadRequest("Collection does not exist");
            
            if(user.Id != collection.UserId)
                return BadRequest("User not authorized for this collection");

            if(!await _collectionRepo.DeleteCollection(collectionId))
                return StatusCode(500, "Collection delete failed");

            return Ok("Collection deleted");
        }

    }
}