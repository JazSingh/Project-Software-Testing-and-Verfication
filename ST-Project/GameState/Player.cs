using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project.GameState
{
    class Player
    {
        private int HPmax;
        private int HP;
        private List<Item> Items;

        public Player()
        {
            //TODO
        }

        public bool use(Dungeon d, Item i)
        {
            return true;
        }
        public bool add(Item i)
        {
            //TODO
            return true;
        }

        public bool save(string filename)
        {
            //TODO
            return true;
        }

        public bool load(string filename)
        {
            return true;
        }

        public void doCombatRound(Dungeon d, Pack p)
        {

        }
    }
}
