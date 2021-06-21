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
    public class GamessController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> GetGames()
        {
            return Ok();
        }

        [HttpGet("id: guild")]
        public async Task<ActionResult<List<object>>> GetGameById(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<List<object>>> InsertGame(object game)
        {
            return Created("", game);
        }

        [HttpPut("id: guild")]
        public async Task<ActionResult<List<object>>> UpdateGame(Guid id, object game)
        {
            return NoContent();
        }

        [HttpPatch("id: guild/price/{price:double}")]
        public async Task<ActionResult<List<object>>> UpdateGamePrice(Guid id, double preco)
        {
            return NoContent();
        }

        [HttpDelete("id: guild")]
        public async Task<ActionResult<List<object>>> DeleteGame(Guid id)
        {
            return NoContent();
        }
    }
}
