using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;

        public StockController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetStucks([FromQuery] QueryObject queryObject)
        {   
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetStocks(queryObject);

            var stocksDTO = stocks.Select(s => s.ToStockDto()).ToList();                          

            return Ok(stocksDTO);
        }

        [HttpGet("{stockId:int}")]
        public async Task<IActionResult> GetStuck(int stockId)
        {   
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _stockRepo.StockExist(stockId))
                return NotFound();

            var stock = await _stockRepo.GetStock(stockId);

            if(stock == null)
                return NotFound();
                
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockRequest stockReq)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockReq.ToStockFromRequest();

            if(await _stockRepo.CreateStock(stockModel) == null)
            {
                ModelState.AddModelError("","Something went worng while creating");
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(CreateStock), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut("{stockId:int}")]
        public async Task<IActionResult> UpdateStuck(int stockId, [FromBody] UpdateStockReq stockReq)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _stockRepo.StockExist(stockId))
                return NotFound();

            Stock ? stock = await _stockRepo.UpdateStock(stockId, stockReq);

            if(stock == null)
                return NotFound();            
                
            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{stockId:int}")]
        public async Task<IActionResult> DeleteStock(int stockId)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(! await _stockRepo.StockExist(stockId))
                 return NotFound();

            await _stockRepo.DeleteStock(stockId);

            return NoContent();
        }

    }
}