using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using ReversiRestApi.DAL;
using ReversiRestApi.Enums;
using ReversiRestApi.Interfaces;
using ReversiRestApi.Models;

namespace ReversiRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository iRepository;
        public GameController(IGameRepository repository)
        {
            iRepository = repository;
        }

        [HttpGet("waiting")]
        public async Task<ActionResult<IEnumerable<ApiGame>>> GetGamesWithWaitingPlayers() => (await iRepository
            .GetGames()).FindAll(x => x.Status == GameStatus.Waiting).Select(ApiGame.GameToApiGame).ToList();

        [HttpGet("waitingNotFull")]
        public async Task<ActionResult<IEnumerable<ApiGame>>> GetGamesWithWaitingPlayersNotFull() => (await iRepository
            .GetGames()).FindAll(x => x.Status == GameStatus.Waiting && x.Player2Token == String.Empty).Select(ApiGame.GameToApiGame).ToList();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiGame>>> GetAllGames() =>
            (await iRepository.GetGames()).Select(ApiGame.GameToApiGame).ToList();

        [HttpGet("{token}")]
        public async Task<ActionResult<ApiGame>> GetGameWithToken(string token)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return NotFound();
            return ApiGame.GameToApiGame(game);
        }

        [HttpPost]
        public async Task<ApiGame> AddNewGame([FromBody] ApiGame game)
        {
            var result = await iRepository.AddGame(new Game()
            {
                Description = game.Description,
                Player1Token = game.Player1Token,
                Status = GameStatus.Waiting,
            });
            return ApiGame.GameToApiGame(result);
        }

        [HttpPut("{token}/join")]
        public async Task<ActionResult<ApiGame>> JoinGame(string token, [FromBody] ApiMove body)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (!game.Join(body.Player))
                return Unauthorized();

            var result = await iRepository.UpdateGame(game);
            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/move")]
        public async Task<ActionResult<ApiGame>> PlayerMove(string token, [FromBody] ApiMove body)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (game.Moving != game.GetPlayerColor(body.Player))
                return Unauthorized();

            if (game.Status != GameStatus.Running)
                return Unauthorized();

            if (!game.Move(body.Row, body.Col))
                return Unauthorized();

            await iRepository.UpdateGame(game);
            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/start")]
        public async Task<ActionResult<ApiGame>> StartGame(string token)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            var startingGame = game.StartGame();

            if (startingGame == false)
                return Forbid();
            await iRepository.UpdateGame(game);

            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/surrender")]
        public async Task<ActionResult<ApiGame>> Surrender(string token, [FromBody] ApiMove body)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (body.Player is null)
                return BadRequest();

            game.SurrenderGame(body.Player);
            return ApiGame.GameToApiGame(game);
        }
    }
}
