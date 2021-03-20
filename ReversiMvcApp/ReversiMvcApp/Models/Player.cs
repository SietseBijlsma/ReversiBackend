using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiMvcApp.Models
{
    public class Player
    {
        [Key] public string Guid { get; set; }
        public string Name { get; set; }
        public int AmountWon { get; set; }
        public int AmountLost { get; set; }
        public int AmountDraw { get; set; }
    }
}
