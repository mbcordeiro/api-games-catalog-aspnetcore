using ApiGamesCatalog.Entities;
using ApiGamesCatalog.InputModels;
using ApiGamesCatalog.Repositories;
using ApiGamesCatalog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> GetGames(int page, int quantity)
        {
            var games = await _gameRepository.GetGames(page, quantity);
            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> GetGameById(Guid id)
        {
            var game = await _gameRepository.GetGameById(id);
            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> InsertGame(GameInputModel game)
        {
            await VerifyExistsGameByNameAndPublisherOrException(game.Name, game.Publisher);

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };

            await _gameRepository.InsertGame(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = gameInsert.Name,
                Publisher = gameInsert.Publisher,
                Price = gameInsert.Price
            };
        }

        public async Task UpdateGame(Guid id, GameInputModel game)
        {
            var gameEntity = await VerifyExistsGameOrException(id);
            
            gameEntity.Name = game.Name;
            gameEntity.Publisher = game.Publisher;
            gameEntity.Price = game.Price;

            await _gameRepository.UpdateGame(gameEntity);
        }

        public async Task UpdateGamePrice(Guid id, double price)
        {
            var gameEntity = await VerifyExistsGameOrException(id);

            gameEntity.Price = gameEntity.Price;

            await _gameRepository.UpdateGame(gameEntity);
        }

        public async Task DeleteGame(Guid id)
        {
            await _gameRepository.DeleteGame(id);    
        }

        public void Dispose()
        {
            _gameRepository.Dispose();
        }

        public async Task<List<Game>> VerifyExistsGameByNameAndPublisherOrException(string name, string publisher)
        {
            var games = await _gameRepository.GetGameByNameAndPublisher(name, publisher);

            if (games.Count > 0)
                throw new Exception("Game already registered");

            return games;
        }

        public async Task<Game> VerifyExistsGameOrException(Guid id)
        {
            var game = await _gameRepository.GetGameById(id);

            if (game == null)
                throw new Exception("Game not register");

            return game;
        }
    }
}
