using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiRestApi.Interfaces;
using ReversiRestApi.Models;

namespace ReversiRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IGameRepository iRepository;

        public PlayerController(IGameRepository repository)
        {
            iRepository = repository;
        }

        [HttpGet("{playerToken}")]
        public async Task<ActionResult<IEnumerable<ApiGame>>> GetAllJoinedGames(string playerToken) => (await iRepository
            .GetGames()).FindAll(x => x.Player1Token == playerToken || x.Player2Token == playerToken)
            .Select(ApiGame.GameToApiGame).ToList();
    }
}
