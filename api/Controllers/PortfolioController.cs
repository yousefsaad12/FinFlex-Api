using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Extenstions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepo _stockRepo;
        private readonly IPortfolioRepo _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepo stockRepo, IPortfolioRepo portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserStocks(user);

            return Ok(userPortfolio);

        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult>AddPortfolio(string Symbol)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);

            var stock = await _stockRepo.GetStockBySymbol(Symbol);

            if(stock == null)
                return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepo.GetUserStocks(user);

            if(userPortfolio.Any(s => s.Symbol.ToLower() == Symbol.ToLower()))
                return BadRequest("Stock is already added");


            var portfolio = new Portfolio
            {
                stockId = stock.Id,
                AppUserId = user.Id
            };

            await _portfolioRepo.CreatePortfolio(portfolio);

            if(portfolio == null)
                return StatusCode(500, "Could Not added");

            return Ok("Stock has been added");
        }


        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepo.GetUserStocks(user);

            var filterdStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower());

            if(filterdStock.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(symbol, user);
            }
            else
                return BadRequest("Stock not in your portfolio");

            return Ok();
        }
    }
}