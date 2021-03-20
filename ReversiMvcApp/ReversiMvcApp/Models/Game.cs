using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiMvcApp.Models
{
    public class Game
    {
        public string Token { get; set; }
        public string Description { get; set; }
        public string Player1Token { get; set; }
        public string Player2Token { get; set; }
        public string Board { get; set; }
        public string Moving { get; set; }
        public string Status { get; set; }
    }
}
