using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiRestApi.Models;

namespace ReversiRestApi.Interfaces
{
    public interface IGameRepository
    {
        Task<Game> AddGame(Game game);
        public Task<List<Game>> GetGames();
        Task<Game> GetGame(string PlayerToken);

        public Task<bool> UpdateGame(Game game);
    }
}
