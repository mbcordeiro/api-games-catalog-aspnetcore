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
using ApiGamesCatalog.Exceptions;

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

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="page">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantity">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.GetGames(1, 10);
            if(games.Count() == 0)
                return NoContent();
            
            return Ok(games);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="id">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>
        [HttpGet("id: guid")]
        public async Task<ActionResult<List<GameViewModel>>> GetGameById([FromRoute]Guid id)
        {
            var game = await _gameService.GetGameById(id);
            if (game == null)
                return NoContent();

            return Ok();
        }

        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="gameInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response> 
        [HttpPost]
        public async Task<ActionResult<List<GameViewModel>>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.InsertGame(gameInputModel);
                return Created("", null);
            }
            catch(GameAlreadySavedException ex)
            {
                return UnprocessableEntity("Error");
            }
        }

        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="game">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPut("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> UpdateGame([FromRoute] Guid id, [FromBody] GameInputModel game)
        {
            try
            {
                await _gameService.UpdateGame(id, game);
                return NoContent(); 
            } 
            catch (GameUnsavedException ex)
            {
                return NotFound("There is already a game with this name for this producer.");
            }
        }

        /// <summary>
        /// Atualizar o preço de um jogo
        /// </summary>
        /// /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="price">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>  
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        [HttpPatch("id: guild/price/{price:double}")]
        public async Task<ActionResult<List<GameViewModel>>> UpdateGamePrice([FromRoute] Guid id, [FromRoute] double price)
        {
            try
            {
                await _gameService.UpdateGamePrice(id, price);
                return NoContent();
            }
            catch (GameUnsavedException ex) 
            {
                return NotFound("Game not found");
            }
        }

        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// /// <param name="id">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response> 
        [HttpDelete("id: guild")]
        public async Task<ActionResult<List<GameViewModel>>> DeleteGame(Guid id)
        {
            try
            {
                await _gameService.DeleteGame(id);
                return NoContent();
            }
            catch (GameUnsavedException ex)
            {
                return NotFound("Game not found");
            }
        }
    }
}
