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

        public CardController(ICardRepository cardRepo, ICollectionRepository collectinoRepo, UserManager<User> userManager)
        {
            _cardRepo = cardRepo;
            _collectionRepo = collectinoRepo;
            _userManager = userManager;
        }

        [HttpGet("{collectionId}")]
        [Authorize]
        public async Task<IActionResult> GetByUserCollectionId([FromRoute] int collectionId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            
            if(user == null)
                return BadRequest("User is null");

            var collection = await _collectionRepo.GetCollectionById(collectionId);
            
            if(collection == null)
                return BadRequest("Collection does not exist");
            
            if(user.Id != collection.UserId)
                return BadRequest("Not authorized for this collection");
            
            var cards = await _cardRepo.GetCardsByCollectionId(collectionId);

            if(cards == null)
                return NotFound("No cards found for this collection");
            
            var cardsDto = cards.Select(c => c.ToCardDto());
            
            return Ok(cardsDto);
        }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCardDto cardDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return BadRequest("User is null");
            
            var collection = await _collectionRepo.GetCollectionById(cardDto.CollectionId);
            
            if(collection == null)
                return BadRequest("Collection does not exist");     
            
            if(user.Id != collection.UserId)
                return BadRequest("Not authorized for this collection");

            var cardModel = cardDto.ToCardFromCreateDto();
            
            if(!await _cardRepo.CreateCard(cardModel))
                return StatusCode(500, "Card create failded");

            return Ok();
        }

        [HttpPut]
        [Route("{cardId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int cardId, [FromBody] UpdateCardDto cardDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);           
            
            if(user == null)
                return BadRequest("User is null");

            var card = await _cardRepo.GetCardById(cardId);        
            if(card == null)
                return BadRequest("Card does not exist");

            var collection = await _collectionRepo.GetCollectionById(card.CollectionId);   
            
            if(collection == null)
                return BadRequest("Collection does not exist");

            if(user.Id != collection.UserId)
                return BadRequest("Not authorized for this collection");
            
            if(!await _cardRepo.UpdateCard(cardId, cardDto.ToCardFromUpdateDto()))
                return StatusCode(500, "Card update failed");

            return Ok();
        }

        [HttpDelete]
        [Route("{cardId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int cardId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return BadRequest("User is null");

            var card = await _cardRepo.GetCardById(cardId);        

            if(card == null)
                return BadRequest("Card does not exist");

            var collection = await _collectionRepo.GetCollectionById(card.CollectionId);   

            if(collection == null)
                return BadRequest("Collection does not exist");

            if(user.Id != collection.UserId)
                return BadRequest("Not authorized for this collection");

            if(!await _cardRepo.DeleteCard(cardId))
                return StatusCode(500, "Card delete failed");

            return Ok();
        }
    }
}