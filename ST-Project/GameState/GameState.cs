using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    public class GameState
    {
        Dungeon d;

        public GameState(int i)
        {
            d = new Dungeon(i);
        }
    }
}
