using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Models
{
    public class ApiMove
    {
        public int Col { get; set; }
        public int Row { get; set; }
        public string Player { get; set; }
        public string Action { get; set; }
    }
}
