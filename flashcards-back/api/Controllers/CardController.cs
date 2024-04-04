using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Card;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepo;
        private readonly ICollectionRepository _collectionRepo;

        public CardController(
                    ICardRepository cardRepo,
                    ICollectionRepository collectinoRepo)
        {
            _cardRepo = cardRepo;
            _collectionRepo = collectinoRepo;
        }

        [HttpGet("card/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cardModel = await _cardRepo.GetByIdAsync(id);

            if (cardModel == null)
            {
                return NotFound();
            }

            var cardDto = cardModel.ToCardDto();
            return Ok(cardDto);
        }

        [HttpGet("collection/{collectionId}")]
        public async Task<IActionResult> GetByCollectionId([FromRoute] int collectionId)
        {
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

            return Ok(cardModel);
        }
    }
}