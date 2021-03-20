using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        public Game AddGame(Game game)
        {
            game.Token = RandomStringGenerator.RandomString(15, true);
            Context.Add(GameModel.FromGame(game));
            Context.SaveChanges();
            return game;
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

        public bool UpdateGame(Game game)
        {
            var local = Context.Games.FirstOrDefault(x => x.ID == game.ID);
            Context.Entry(local).State = EntityState.Detached;
            local = GameModel.FromGame(game);

            Context.Entry(local).State = EntityState.Modified;
            return Context.SaveChanges() > 0;
        }
    }
}
