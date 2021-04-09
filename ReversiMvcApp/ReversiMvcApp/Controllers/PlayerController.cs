using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ReversiDbContext _context;

        public PlayerController(ReversiDbContext context)
        {
            _context = context;
        }

        private bool PlayerExists(string id)
        {
            return _context.Players.Any(e => e.Guid == id);
        }

        public async Task<Player> GetLoggedInPlayer(Controller origin)
        {
            return await GetPlayerOrCreate(origin.User);
        }

        public async Task<Player> GetLoggedInPlayer(ClaimsPrincipal user)
        {
            return await GetPlayerOrCreate(user);
        }

        public async Task<Player> GetPlayerOrCreate(ClaimsPrincipal user)
        {
            var guid = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var name = user.FindFirst(ClaimTypes.Name).Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            return GetPlayer(guid) ?? await CreatePlayer(guid, name, email ?? name);
        }

        public async Task<Player> CreatePlayer(string name, string guid, string email)
        {
            Player player = new Player()
            {
                Guid = name,
                Name = guid,
                AmountWon = 0,
                AmountDraw = 0,
                AmountLost = 0,
                Email = email
            };
            _context.Players.Add(player);
            _context.SaveChangesAsync();
            return player;
        }

        public async Task SavePlayer(Player player)
        {
            var current =  await _context.Players.FirstOrDefaultAsync(x => x.Email == player.Email);
            _context.Entry(current).State = EntityState.Detached;

            current = player;

            _context.Entry(current).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public Player GetPlayer(string guid)
        {
            return _context.Players.FirstOrDefault(x => x.Guid == guid);
        }

        public Player GetPlayer(ClaimsPrincipal user)
        {
            var guid = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            return GetPlayer(guid);
        }
        public async Task DeletePlayer(string guid)
        {
            var player = _context.Players.FirstOrDefault(x => x.Guid == guid);

            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }
    }
}
