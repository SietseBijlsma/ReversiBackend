using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReversiRestApi.Enums;

namespace ReversiRestApi.Models
{
    public class ApiGame
    {
        public string Description { get; set; }
        public string Token { get; set; }
        public string Player1Token { get; set; }
        public string Player2Token { get; set; }
        public string Board { get; set; }
        public string Moving { get; set; }
        public string Status { get; set; }
        public string Winner { get; set; }

        public static ApiGame GameToApiGame(Game game)
        {
            return new ApiGame()
            {
                Board = JsonConvert.SerializeObject(game.Board),
                Description = game.Description,
                Token = game.Token,
                Player1Token = game.Player1Token,
                Player2Token = game.Player2Token,
                Moving = game.Moving.ToString(),
                Status = game.Status.ToString(),
                Winner = game.Winner
            };
        }
    }
}
