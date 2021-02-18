using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReversiRestApi.Interfaces;
using ReversiRestApi.Models;
using ReversiRestApi.Models.Databse;

namespace ReversiRestApi.DAL
{
    public class GameAccessLayer : IGameRepository
    {
        private DatabaseContext Context { get; }
        private DbSet<GameModel> Games { get; }

        public GameAccessLayer(DatabaseContext context)
        {
            Context = context;
            Games = Context.Games;
        }

        public void AddGame(Game game)
        {
            game.Token = RandomStringGenerator.RandomString(15, true);
            Context.Add(GameModel.FromGame(game));
            Context.SaveChanges();
        }

        public Game GetGame(string gameToken)
        {
            return Games.FirstOrDefault(x => x.Token == gameToken).ToGame();
        }

        public IList<Game> GetGameWithPlayerToken(string playerToken)
        {
            return Games.Where(x => x.Player1Token == playerToken || x.Player2Token == playerToken)
                .Select(x => x.ToGame()).ToList();
        }

        public List<Game> GetGames()
        {
            return Games.Select(x => x.ToGame()).ToList();
        }
    }
}
