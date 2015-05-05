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
        Player p;
        int time;

        public GameState(int i)
        {
            d = new Dungeon(i);
            p = new Player();
            time = 0;
            Console.WriteLine(d.ToString());
            Console.WriteLine(p.ToString());
        }

        public Dungeon GetDungeon()
        {
            return d;
        }

        public Player GetPlayer()
        {
            return p;
        }

        public int GetPosition()
        {
            return p.get_position();
        }

        public void SetPosition(int i)
        {
            p.set_position(i);
        }
    }
}
