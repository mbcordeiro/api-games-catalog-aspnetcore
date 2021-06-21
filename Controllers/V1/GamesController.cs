using ApiGamesCatalog.InputModels;
using ApiGamesCatalog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> GetGames()
        {
            return Ok();
        }

        [HttpGet("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> GetGameById(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<List<GameViewModel>>> InsertGame(GameInputModel game)
        {
            return Created("", game);
        }

        [HttpPut("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> UpdateGame(Guid id, GameInputModel game)
        {
            return NoContent();
        }

        [HttpPatch("id: guild/price/{price:double}")]
        public async Task<ActionResult<List<GameViewModel>>> UpdateGamePrice(Guid id, double preco)
        {
            return NoContent();
        }

        [HttpDelete("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> DeleteGame(Guid id)
        {
            return NoContent();
        }
    }
}
