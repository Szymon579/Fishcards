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
        
        public CollectionController(ICollectionRepository collectionRepo, 
                                    UserManager<User> userManager) 
        {
            _collectionRepo = collectionRepo;
            _userManager = userManager;
        }

        [HttpGet("authorized")]
        [Authorize]
        public async Task<IActionResult> GetUserCollections()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var userCollections = await _collectionRepo.GetUserCollections(user);
            return Ok(userCollections);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var collections = await _collectionRepo.GetAllAsync();
                
            var collectionsDto = collections.Select(s => s.ToCollectionDto());
            
            return Ok(collectionsDto);
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var collectionModel = await _collectionRepo.GetByIdAsync(id);
            
            if (collectionModel == null)
            {
                return NotFound();
            }

            var collectionDto = collectionModel.ToCollectionWithCardsDto();

            return Ok(collectionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCollectionDto collectionDto) 
        {
            var collectionModel = collectionDto.ToCollectionFromCreateDto();
            await _collectionRepo.CreateAsync(collectionModel);

            return CreatedAtAction(nameof(GetById), new {id = collectionModel.Id}, collectionModel.ToCollectionDto());
        }

        [HttpPost("authorized")]
        [Authorize]
        public async Task<IActionResult> CreateByUser([FromBody] CreateCollectionDto collectionDto) 
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            var collection = new Collection
            {
                UserId = user.Id,
                Title = collectionDto.Title
            };

            await _collectionRepo.CreateByUser(collection);

            if(collection == null)
            {
                return StatusCode(500, "Could not create for user");
            }

            return Ok(collection.ToCollectionDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var collectionModel = await _collectionRepo.DeleteAsync(id);

            if (collectionModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}