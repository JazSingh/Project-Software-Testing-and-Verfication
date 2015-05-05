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
            d.SpawnMonsters();
            Console.WriteLine(d.ToString());
            Console.WriteLine(p.ToString());
            Console.WriteLine("Total hp monsters: " + d.SumMonsterHealth());
            Console.WriteLine("Total hp player and pots: " + SumPlayerPotsHP());
        }

        public Dungeon GetDungeon()
        {
            return d;
        }

        public Player GetPlayer()
        {
            return p;
        }

        private int SumPlayerPotsHP()
        {
            return p.GetHP() + d.SumHealPots();

        }

        private void DropHealthPot()
        {
            if (SumPlayerPotsHP() >= d.SumMonsterHealth() && Oracle.Decide())
                d.DropHealthPot();
        }

        public void SetPosition(int i)
        {
            p.set_position(i);
        }

    }
}
