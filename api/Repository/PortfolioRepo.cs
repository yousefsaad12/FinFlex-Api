using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepo : IPortfolioRepo
    {
        private readonly AppDbContext _appContext;

        public PortfolioRepo(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await _appContext.Portfolios.AddAsync(portfolio);
            await _appContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolio(string symbol, AppUser appUser)
        {
            var portfolioModel = await _appContext.Portfolios
                                                  .FirstOrDefaultAsync(u => u.AppUserId == appUser.Id && u.Stock.Symbol.ToLower() == symbol.ToLower());

            if(portfolioModel == null)
                return null;

            _appContext.Portfolios.Remove(portfolioModel);
            await _appContext.SaveChangesAsync();

            return portfolioModel;

        }

        public async Task<List<Stock>> GetUserStocks(AppUser appUser)
        {
            return await _appContext.Portfolios
                                    .Where(u => u.AppUserId == appUser.Id)
                                    .Select(stock => new Stock
                                    {
                                        Id = stock.stockId,
                                        Symbol = stock.Stock.Symbol,
                                        Industry = stock.Stock.Industry,
                                        CompanyName = stock.Stock.CompanyName,
                                        Purchase = stock.Stock.Purchase,
                                        LastDiv = stock.Stock.LastDiv,
                                        MarketCap = stock.Stock.MarketCap
                                    })
                                    .ToListAsync();
        }
    }
}