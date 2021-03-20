using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiRestApi.Enums;
using ReversiRestApi.Interfaces;

namespace ReversiRestApi.Models
{
    public class GameRepository : IGameRepository
    {
        public List<Game> Games { get; set; }
        public GameRepository()
        {
            Game Game1 = new Game();
            Game Game2 = new Game();
            Game Game3 = new Game();
            Game1.Player1Token = "abcdef";
            Game1.Description = "Potje snel reveri, dus niet lang nadenken";
            Game2.Player1Token = "ghijkl";
            Game2.Player2Token = "mnopqr";
            Game2.Description = "Ik zoek een gevorderde tegenGameer!";
            Game3.Player1Token = "stuvwx";
            Game3.Description = "Na dit Game wil ik er nog een paar spelen tegen zelfde tegenstander";
            Game1.Token = "aaaaa";
            Game2.Token = "bbbbb";
            Game3.Token = "ccccc";
            Game1.Status = GameStatus.Waiting;
            Game2.Status = GameStatus.Running;
            Game3.Status = GameStatus.Waiting;
            Games = new List<Game> { Game1, Game2, Game3 };
        }

        public Game AddGame(Game game)
        {
            Games.Add(game);
            game.Token = RandomStringGenerator.RandomString(15, true);
            game.Status = GameStatus.Waiting;
            return game;
        }

        public List<Game> GetGames() => Games;
        public Game GetGame(string GameToken) => Games.FirstOrDefault(x => x.Token == GameToken);

        public bool UpdateGame(Game game)
        {
            return true;
        }
    }

}
