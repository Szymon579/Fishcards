using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Card;
using api.Dtos.CardsCollection;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    
    [Route("api/collections")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepo;
        
        public CollectionController(ICollectionRepository collectionRepo) 
        {
            _collectionRepo = collectionRepo;
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