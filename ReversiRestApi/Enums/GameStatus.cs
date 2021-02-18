using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Enums
{
    public enum GameStatus
    {
        Invalid, //invalid
        Finished, //game is finished
        Waiting, //game hasn't started yet
        Running  //game is playing
    }
}
