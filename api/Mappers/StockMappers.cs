using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
         public static StockDTO ToStockDto(this Stock stock)
         {
            return new StockDTO ()
            {
                Id = stock.Id,
                CompanyName = stock.CompanyName,
                Symbol = stock.Symbol,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase, 
                Industry = stock.Industry,
                Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
            };
         }

         public static Stock ToStockFromRequest(this StockRequest stockRequest)
         {
            return new Stock ()
            {
                CompanyName = stockRequest.CompanyName,
                Symbol = stockRequest.Symbol,
                LastDiv = stockRequest.LastDiv,
                MarketCap = stockRequest.MarketCap,
                Purchase = stockRequest.Purchase, 
                Industry = stockRequest.Industry
            };
         }
    }   
}