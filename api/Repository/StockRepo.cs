using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepo : IStockRepo
    {
        private readonly AppDbContext _appDbContext;

        public StockRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Stock> CreateStock(Stock stock)
        {
            await _appDbContext.Stocks.AddAsync(stock);
            await _appDbContext.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> DeleteStock(int stockId)
        {
            Stock ? stock = await _appDbContext.Stocks.FirstOrDefaultAsync(s => s.Id == stockId);

            if(stock == null)
                return null;

            _appDbContext.Stocks.Remove(stock);
            await _appDbContext.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> GetStock(int stockId)
        {
            return await _appDbContext.Stocks.Include(c => c.Comments)
                                             .FirstOrDefaultAsync(s => s.Id == stockId);
        }

        public async Task<Stock?> GetStockBySymbol(string symbol)
        {
            return await _appDbContext.Stocks
                                      .FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<List<Stock>> GetStocks(QueryObject query)
        {
            var stocks = _appDbContext.Stocks
                                      .Include(c => c.Comments)
                                      .ThenInclude(a => a.AppUser)
                                      .AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

            if(!string.IsNullOrWhiteSpace(query.Symbol))
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : 
                             stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        

        public async Task<bool> StockExist(int stockId)
        {
            return await _appDbContext.Stocks.AnyAsync(s => s.Id == stockId);
        }

        public async Task<Stock?> UpdateStock(int stockId,UpdateStockReq stockReq)
        {
            var stock = await _appDbContext.Stocks.FindAsync(stockId);

            if(stock == null)
                return null;

            stock.Industry = stockReq.Industry;
            stock.Purchase = stockReq.Purchase;
            stock.Symbol = stockReq.Symbol;
            stock.MarketCap = stockReq.MarketCap;
            stock.CompanyName = stockReq.CompanyName;
            stock.LastDiv = stockReq.LastDiv;

            await _appDbContext.SaveChangesAsync();
            
            return stock;
        }
    }
}