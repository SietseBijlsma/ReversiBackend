using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiRestApi.Models;

namespace ReversiRestApi.Interfaces
{
    public interface IGameRepository
    {
        Game AddGame(Game game);
        public List<Game> GetGames();
        Game GetGame(string PlayerToken);

        public bool UpdateGame(Game game);
    }
}
