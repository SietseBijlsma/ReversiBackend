using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Game> AddGame(Game game)
        {
            game.Token = RandomStringGenerator.RandomString(15, true);
            Context.Add(GameModel.FromGame(game));
            await Context.SaveChangesAsync();
            return game;
        }

        public async Task<Game> GetGame(string gameToken)
        {
            var game = await Games.FirstOrDefaultAsync(x => x.Token == gameToken);
            return game.ToGame();
        }

        public async Task<IList<Game>> GetGameWithPlayerToken(string playerToken)
        {
            return await Games.Where(x => x.Player1Token == playerToken || x.Player2Token == playerToken)
                .Select(x => x.ToGame()).ToListAsync();
        }

        public async Task<List<Game>> GetGames()
        {
            return await Games.Select(x => x.ToGame()).ToListAsync();
        }

        public async Task<bool> UpdateGame(Game game)
        {
            var local = await Context.Games.FirstOrDefaultAsync(x => x.ID == game.ID);
            Context.Entry(local).State = EntityState.Detached;
            local = GameModel.FromGame(game);

            Context.Entry(local).State = EntityState.Modified;
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
