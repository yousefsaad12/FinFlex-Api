using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetStocks (QueryObject queryObject);
        Task<bool> StockExist(int stockId);
        Task<Stock?> GetStock(int stockId);
        Task<Stock> CreateStock(Stock stock);
        Task<Stock?> UpdateStock(int stockId,UpdateStockReq updateStock);
        Task<Stock?> DeleteStock(int stockId);

        Task<Stock?> GetStockBySymbol(string symbol);
        

    }
}