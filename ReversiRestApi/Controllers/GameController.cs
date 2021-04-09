using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                return StatusCode((int) HttpStatusCode.NotFound, "Could not find game");
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
                return StatusCode((int)HttpStatusCode.NotFound, "Could not find game");

            if (!game.Join(body.Player))
                return StatusCode((int)HttpStatusCode.Unauthorized, "Not allowed to join");

            var result = await iRepository.UpdateGame(game);
            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/move")]
        public async Task<ActionResult<ApiGame>> PlayerMove(string token, [FromBody] ApiMove body)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return StatusCode((int)HttpStatusCode.NotFound, "Could not find game");

            if (game.Moving != game.GetPlayerColor(body.Player))
                return StatusCode((int)HttpStatusCode.Unauthorized, "It is not your move");

            if (game.Status != GameStatus.Running)
                return StatusCode((int)HttpStatusCode.Unauthorized, "Game hasn't started yet");

            if (!game.Move(body.Row, body.Col))
                return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid move");

            await iRepository.UpdateGame(game);
            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/start")]
        public async Task<ActionResult<ApiGame>> StartGame(string token)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return StatusCode((int)HttpStatusCode.NotFound, "Could not find game");

            var startingGame = game.StartGame();

            if (startingGame == false)
                return StatusCode((int)HttpStatusCode.Unauthorized, "Could not start game");
            await iRepository.UpdateGame(game);

            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/surrender")]
        public async Task<ActionResult<ApiGame>> Surrender(string token, [FromBody] ApiMove body)
        {
            var game = await iRepository.GetGame(token);
            if (game is null)
                return StatusCode((int)HttpStatusCode.NotFound, "Could not find game");

            if (body.Player is null)
                return StatusCode((int)HttpStatusCode.NotFound, "No player found");

            game.SurrenderGame(body.Player);
            await iRepository.UpdateGame(game);
            return ApiGame.GameToApiGame(game);
        }
    }
}
