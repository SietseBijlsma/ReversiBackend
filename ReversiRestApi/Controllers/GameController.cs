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
        public ActionResult<IEnumerable<ApiGame>> GetGamesWithWaitingPlayers() => iRepository
            .GetGames().FindAll(x => x.Status == GameStatus.Waiting).Select(ApiGame.GameToApiGame).ToList();

        [HttpGet("waitingNotFull")]
        public ActionResult<IEnumerable<ApiGame>> GetGamesWithWaitingPlayersNotFull() => iRepository
            .GetGames().FindAll(x => x.Status == GameStatus.Waiting && x.Player2Token == String.Empty).Select(ApiGame.GameToApiGame).ToList();

        [HttpGet]
        public ActionResult<IEnumerable<ApiGame>> GetAllGames() =>
            iRepository.GetGames().Select(ApiGame.GameToApiGame).ToList();

        [HttpGet("{token}")]
        public ActionResult<ApiGame> GetGameWithToken(string token)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();
            return ApiGame.GameToApiGame(game);
        }

        [HttpPost]
        public ApiGame AddNewGame([FromBody] ApiGame game)
        {
            var result = iRepository.AddGame(new Game()
            {
                Description = game.Description,
                Player1Token = game.Player1Token,
                Status = GameStatus.Waiting,
            });
            return ApiGame.GameToApiGame(result);
        }

        [HttpPut("{token}/join")]
        public ActionResult<ApiGame> JoinGame(string token, [FromBody] ApiMove body)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (!game.Join(body.Player))
                return Unauthorized();

            var result = iRepository.UpdateGame(game);
            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/move")]
        public ActionResult<ApiGame> PlayerMove(string token, [FromBody] ApiMove body)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (game.Moving != game.GetPlayerColor(body.Player))
                return Unauthorized();

            if (game.Status != GameStatus.Running)
                return Unauthorized();

            if (!game.Move(body.Row, body.Col))
                return Unauthorized();

            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/start")]
        public ActionResult<ApiGame> StartGame(string token)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            var startingGame = game.StartGame();

            if (startingGame == false)
                return Forbid();

            return ApiGame.GameToApiGame(game);
        }

        [HttpPut("{token}/surrender")]
        public ActionResult<ApiGame> Surrender(string token, [FromBody] ApiMove body)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (body.Player is null)
                return BadRequest();

            game.SurrenderGame(body.Player);
            return ApiGame.GameToApiGame(game);
        }
    }
}
