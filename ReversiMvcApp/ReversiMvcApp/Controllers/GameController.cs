using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ReversiRestApi.Models;

namespace ReversiMvcApp.Controllers
{
    public class GameController : Controller
    {
        private readonly ApiController _apiController;

        public GameController(ApiController apiController)
        {
            _apiController = apiController;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string description)
        {
            var result = await _apiController.PostAsync(new ApiGame()
            {
                Description = description,
                Player1Token = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            }, "game");
            return RedirectToAction("Board", new {token = result.Token});
        }

        [Authorize]
        public async Task<IActionResult> Waiting()
        {
            var games = await _apiController.GetList<ApiGame>("game/waitingNotFull");

            return View(games);
        }

        [Authorize]
        [HttpGet("[controller]/joined")]
        public async Task<IActionResult> Joined()
        {
            var user = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var games = await _apiController.GetList<ApiGame>("player/" + user);
            return View(games);
        }

        [Authorize]
        [HttpGet("[controller]/{token}/join")]
        public async Task<IActionResult> Join(string token)
        {
            await _apiController.PutAsync(new ApiMove()
            {
                Player = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            }, "game/" + token + "/join");

            return RedirectToAction("Board", new {token = token});
        }

        [HttpGet("[controller]/{token}/start")]
        public async Task<IActionResult> Start(string token)
        {
            await _apiController.PutAsync<ApiGame>(new ApiGame(), "game/" + token + "/start");

            return RedirectToAction("Board", new {token = token});
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("[controller]/{token}")]
        public async Task<IActionResult> Board(string token)
        {
            var game = await _apiController.GetAsync<ApiGame>("game/" + token);
            return View(game);
        }
    }
}
