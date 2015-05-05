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
            DropItems();
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

        private bool DropHealthPot()
        {
            bool dropped = false;
            if (SumPlayerPotsHP()< d.SumMonsterHealth() && Oracle.Decide())
            {
                d.DropItem(ItemType.HealthPotion);
                dropped = true;
            }
            return dropped;
        }

        public void SetPosition(int i)
        {
            p.set_position(i);
        }

        private void DropItems()
        {
            int i = 1;
            int k = Oracle.GiveNumber(d.difficulty, d.difficulty + 2);

            while (i <= k)
            {
                int r = Oracle.GiveNumber(2);
                switch (r)
                {
                    case 0: d.DropItem(ItemType.MagicScroll); break;
                    case 1: d.DropItem(ItemType.TimeCrystal); break;
                    default: if (SumPlayerPotsHP() < d.SumMonsterHealth()) d.DropItem(ItemType.HealthPotion); break;
                }
                i++;
            }
        }
        public bool CheckFinished()
        {
            if (GetPlayer().get_position() == GetDungeon().nodes.Length - 1)
            {
                Console.WriteLine("Reached end node!");
                return true;
            }
            return false;
        }

    }
}
