using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReversiRestApi.Enums;

namespace ReversiRestApi.Models.Databse
{
    public class GameModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public string Player1Token { get; set; }
        [AllowNull] public string Player2Token { get; set; }
        public Color Player1Color { get; set; }
        public Color Player2Color { get; set; }
        public string Board { get; set; }
        public Color Moving { get; set; }
        public int Size { get; set; }
        public int MoveCount { get; set; }
        public GameStatus Status { get; set; }
        [AllowNull] public string Winner { get; set; }

        public Game ToGame()
        {
            return new Game()
            {
                Token = this.Token,
                ID = this.ID,
                Description = this.Description,
                Player1Token = this.Player1Token,
                Player2Token = this.Player2Token,
                Moving = this.Moving,
                Board = JsonConvert.DeserializeObject<Color[,]>(this.Board),
                Status = this.Status,
                MoveCount = this.MoveCount,
                Winner = this.Winner,
                Size = this.Size,
                Player1Color = this.Player1Color,
                Player2Color = this.Player2Color
            };
        }

        public static GameModel FromGame(Game game)
        {
            return new GameModel()
            {
                ID = game.ID,
                Token = game.Token,
                Description = game.Description,
                Player1Token = game.Player1Token,
                Player2Token = game.Player2Token ?? "",
                Moving = game.Moving,
                Board = JsonConvert.SerializeObject(game.Board),
                Status = game.Status,
                MoveCount = game.MoveCount,
                Winner = game.Winner,
                Size = game.Size,
                Player1Color = game.Player2Token != null ? game.Player1Color : Color.None,
                Player2Color = game.Player2Token != null ? game.Player1Color : Color.None
            };
        }
    }
}
