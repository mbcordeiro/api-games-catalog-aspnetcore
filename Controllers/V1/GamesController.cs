using ApiGamesCatalog.InputModels;
using ApiGamesCatalog.Services;
using ApiGamesCatalog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.GetGames(1, 10);
            if(games.Count() == 0)
                return NoContent();
            
            return Ok(games);
        }

        [HttpGet("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> GetGameById([FromRoute]Guid id)
        {
            var game = await _gameService.GetGameById(id);
            if (game == null)
                return NoContent();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<List<GameViewModel>>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.InsertGame(gameInputModel);
                return Created("", null);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity("Error");
            }
        }

        [HttpPut("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> UpdateGame([FromRoute] Guid id, [FromBody] GameInputModel game)
        {
            try
            {
                await _gameService.UpdateGame(id, game);
                return NoContent(); 
            } 
            catch (Exception ex)
            {
                return NotFound("Game not found");
            }
        }

        [HttpPatch("id: guild/price/{price:double}")]
        public async Task<ActionResult<List<GameViewModel>>> UpdateGamePrice([FromRoute] Guid id, [FromRoute] double price)
        {
            try
            {
                await _gameService.UpdateGamePrice(id, price);
                return NoContent();
            }
            catch (Exception ex) 
            {
                return NotFound("Game not found");
            }
        }

        [HttpDelete("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> DeleteGame(Guid id)
        {
            try
            {
                await _gameService.DeleteGame(id);
                return NoContent();
            }
            catch
            {
                return NotFound("Game not found");
            }
        }
    }
}
