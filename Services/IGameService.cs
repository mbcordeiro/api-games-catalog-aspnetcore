using ApiGamesCatalog.InputModels;
using ApiGamesCatalog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Services
{ 
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetGames(int page, int quantity);
        Task<GameViewModel> GetGameById(Guid id);
        Task<GameViewModel> InsertGame(GameInputModel game);
        Task UpdateGame(Guid id, GameInputModel game);
        Task UpdateGamePrice(Guid id, double price);
        Task DeleteGame(Guid id);
    }
}
