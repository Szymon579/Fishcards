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
using api.Dtos.ShareCollection;
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
        private readonly ISharedCollectionRepository _sharedCollectionRepo;
        private readonly UserManager<User> _userManager;
        
        public CollectionController(ICollectionRepository collectionRepo, ISharedCollectionRepository sharedCollectionRepository, UserManager<User> userManager) 
        {
            _collectionRepo = collectionRepo;
            _sharedCollectionRepo = sharedCollectionRepository;
            _userManager = userManager;
        }

        [HttpPost("share")]     
        [Authorize]
        public async Task<IActionResult> Share([FromBody] ShareCollecitonDto shareDto)
        {
            var username = User.GetUsername();
            var owner = await _userManager.FindByNameAsync(username);

            if(owner == null)
            {
                return BadRequest("User is null");
            }

            var collection = await _collectionRepo.GetByIdAsync(shareDto.CollectionId);
            
            if (collection == null)
            {
                return NotFound("Collection not found!");
            }

            if(owner.Id != collection.UserId)
            {
                return BadRequest("Not autorized for this collection");
            }

            var shareToUser = await _userManager.FindByEmailAsync(shareDto.UserEmail);

            if(shareToUser == null)
            {
                return BadRequest("Share to user not found");
            }

            var share = new SharedCollection
            {
                CollectionId = shareDto.CollectionId,
                SharedWithUserId = shareToUser.Id,
                CanEdit = false
            };

            var shareResult = await _sharedCollectionRepo.Share(share);

            if(shareResult == null)
            {
                return NotFound("Share failed");
            }

            return Ok(shareResult);
        }


        [HttpGet("shared")]
        [Authorize]
        public async Task<IActionResult> GetSharedCollections()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return BadRequest("User is null");
            }

            var shared = await _sharedCollectionRepo.GetShared(user);
            
            if(shared == null)
            {
                return NotFound("Shared collections not found");
            }

            return Ok(shared.Select(c => c.ToCollectionWithCardsDto()));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserCollections()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return BadRequest("User is null");
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
                return NotFound("Collection not found");
            }

            if(user.Id != collection.UserId)
            {
                return BadRequest("User not authorized for this collection");
            }

            return Ok(collection.ToCollectionWithCardsDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCollectionDto collectionDto) 
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
            {
                return BadRequest("User is null");
            }

            var collection = collectionDto.ToCollectionFromCreateDto(user.Id);

            await _collectionRepo.CreateAsync(collection);

            return Ok(collection.ToCollectionDto());
        }

        [HttpPut]
        [Route("{collectionId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int collectionId, [FromBody] UpdateCollectionDto collectionDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return BadRequest("User is null");
            }

            var existingCollection = await _collectionRepo.GetByIdAsync(collectionId);
            
            if(existingCollection == null)
            {
                return BadRequest("Collection does not exist");
            }     
            
            if(user.Id != existingCollection.UserId)
            {
                return BadRequest("Not authorized for this collection");
            }

            var updatedCollection = await _collectionRepo.UpdateAsync(collectionId, collectionDto.ToCollectionFromUpdateDto());

            if(updatedCollection == null)
            {
                return NotFound("Collection update failed");
            }

            return Ok(updatedCollection.ToCollectionDto());
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

            return Ok("Collection deleted");
        }

    }
}