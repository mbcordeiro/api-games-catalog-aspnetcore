using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Exceptions
{
    public class GameAlreadySavedException : Exception
    {
        public GameAlreadySavedException() : base("This game has already been saved in the database")
        {
        }
    }
}
