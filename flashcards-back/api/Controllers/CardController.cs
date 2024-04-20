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
            {
                return BadRequest("User is null");
            }

            var collection = await _collectionRepo.GetByIdAsync(collectionId);
            
            if(collection == null)
            {
                return BadRequest("Collection does not exist");
            }
            
            if(user.Id != collection.UserId)
            {
                return BadRequest("Not authorized for this collection");
            }
            
            var cards = await _cardRepo.GetByCollectionIdAsync(collectionId);

            if(cards == null)
            {
                return NotFound();
            }
            
            var cardsDto = cards.Select(c => c.ToCardDto());
            return Ok(cardsDto);
        }
        

        [HttpPost()]
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

        [HttpPut]
        [Route("{cardId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int cardId, [FromBody] UpdateCardDto cardDto)
        {
            var card = await _cardRepo.UpdateAsync(cardId, cardDto.ToCardFromUpdateDto());

            if(card == null)
            {
                return NotFound("Card not found");
            }

            return Ok(card.ToCardDto());
        }

        [HttpDelete]
        [Route("{cardId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int cardId)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return BadRequest("User is null");
            }

            var card = await _cardRepo.GetByIdAsync(cardId);

            if(card == null)
            {
                return BadRequest("Card does not exist");
            }

            var collection = await _collectionRepo.GetByIdAsync(card.CollectionId);
            
            if(user.Id != collection.UserId)
            {
                return BadRequest("Not authorized for this card");
            }

            await _cardRepo.Delete(cardId);

            return Ok(card.ToCardDto());
        }
    }
}