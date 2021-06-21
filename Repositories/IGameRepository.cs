using ApiGamesCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Repositories
{
    interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetGames(int page, int quantity);
        Task<Game> GetGameById(Guid id);
        Task<List<Game>> GetGameByNameAndPublisher(string name, string publisher);
        Task InsertGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid id);
    }
}
