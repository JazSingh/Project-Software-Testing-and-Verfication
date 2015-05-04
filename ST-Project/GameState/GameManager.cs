using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{

    class GameManager
    {
        GameState state;

        public GameManager(int i)
        {
            state = new GameState(i);
        }
    }
}
