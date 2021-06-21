using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Exceptions
{
    public class GameUnsavedException : Exception
    {
        public GameUnsavedException() : base("This game was not saved in the database")
        {
        }
    }
}
