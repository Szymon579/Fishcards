using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Card;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepo;
        private readonly ICollectionRepository _collectionRepo;
        private readonly UserManager<User> _userManager;

        public CardController(
                    ICardRepository cardRepo,
                    ICollectionRepository collectinoRepo,
                    UserManager<User> userManager
                    )
        {
            _cardRepo = cardRepo;
            _collectionRepo = collectinoRepo;
            _userManager = userManager;
        }

        [HttpGet("card/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cardModel = await _cardRepo.GetByIdAsync(id);

            if (cardModel == null)
            {
                return NotFound();
            }
          
            return Ok(cardModel.ToCardDto());
        }

        [HttpGet("authorized/collection/{collectionId}")]
        [Authorize]
        public async Task<IActionResult> GetByUserCollectionId([FromRoute] int collectionId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
            {
                return BadRequest("User is null");
            }

            var collection = await _collectionRepo.GetByIdAsync(collectionId);
            if(user.Id != collection.UserId)
            {
                return BadRequest("Not authorized for this collection");
            }
            
            if(!await _collectionRepo.CollectionExist(collectionId))
            {
                return BadRequest("Collection does not exist");
            }
            
            var cardsModels = await _cardRepo.GetByCollectionIdAsync(collectionId);

            if(cardsModels == null)
            {
                return NotFound();
            }
            
            var cardsDto = cardsModels.Select(c => c.ToCardDto());
            return Ok(cardsDto);
        }
        
        [HttpGet("collection/{collectionId}")]
        public async Task<IActionResult> GetByCollectionId([FromRoute] int collectionId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
            {
                return BadRequest("User is null");
            }

            if(!await _collectionRepo.CollectionExist(collectionId))
            {
                return BadRequest("Collection does not exist");
            }
            
            var collection = await _collectionRepo.GetByIdAsync(collectionId);
            if(user.Id != collection.UserId)
            {
                return BadRequest("Not authorized for this collection");
            }

            var cardsModels = await _cardRepo.GetByCollectionIdAsync(collectionId);

            if(cardsModels == null)
            {
                return NotFound();
            }
            
            var cardsDto = cardsModels.Select(c => c.ToCardDto());
            return Ok(cardsDto);
        }

        [HttpPost("authorized")]
        [Authorize]
        public async Task<IActionResult> CreateByUser([FromBody] CreateCardDto cardDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return BadRequest("User is null");
            }
            
            if(!await _collectionRepo.CollectionExist(cardDto.CollectionId))
            {
                return BadRequest("Collection does not exist");
            }

            var collection = await _collectionRepo.GetByIdAsync(cardDto.CollectionId);
            if(user.Id != collection.UserId)
            {
                return BadRequest("Not authorized for this collection");
            }

            var cardModel = cardDto.ToCardFromCreateDto();
            await _cardRepo.CreateAsync(cardModel);

            return Ok(cardModel.ToCardDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCardDto cardDto)
        {
            Console.WriteLine("Collection id" + cardDto.CollectionId);
            
            if(!await _collectionRepo.CollectionExist(cardDto.CollectionId))
            {
                return BadRequest("Collection does not exist");
            }

            var cardModel = cardDto.ToCardFromCreateDto();
            await _cardRepo.CreateAsync(cardModel);

            return Ok(cardModel.ToCardDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cardModel = await _cardRepo.Delete(id);
            
            if(cardModel == null)
            {
                return BadRequest("Card does not extist");
            }    

            return Ok(cardModel.ToCardDto());
        }
    }
}