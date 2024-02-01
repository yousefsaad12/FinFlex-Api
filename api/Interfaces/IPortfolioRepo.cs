using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepo
    {
        Task<List<Stock>> GetUserStocks(AppUser appUser);
        Task<Portfolio> CreatePortfolio(Portfolio portfolio);

        Task<Portfolio?> DeletePortfolio(string symbol, AppUser appUser);
    }
}