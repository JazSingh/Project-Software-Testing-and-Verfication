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

        // Load constructor
        public GameState(Dungeon dungeon, Player player)
        {
            d = dungeon;
            p = player;
            d.SpawnMonsters();
            DropItems();
        }

        public void Save(string filename)
        {
            d = new Dungeon(d.difficulty + 1);
            p.save(d, filename);
        }

        public Dungeon GetDungeon()
        {
            return d;
        }

        public Player GetPlayer()
        {
            return p;
        }

        public bool CheckRetreat()
        {
            if (d.CheckRetreat(p.get_position()))
                return true;
            return false;
        }

        public bool fighting()
        {
            int pos = GetPlayer().get_position();
            if (GetDungeon().GetNode(pos).hasPack())
            {
                return true;
            }
            return false;
        }

        private int SumPlayerPotsHP()
        {
            return p.GetHP() + d.SumHealPots();
        }

        public void NextLevel()
        {
            d = new Dungeon(d.difficulty == 5 ? 5 : d.difficulty+1);
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
            CheckItemsFound(i);
        }

        private void CheckItemsFound(int i)
        {
            List<Item> items = d.GetNode(i).get_Items();
            foreach(Item j in items)
            {
                p.add(j);
                Console.WriteLine("Item gevonden!");
            }
            d.nodes[i].RemoveItems();
        }

        public bool CheckFinished()
        {
            if (p.get_position() == GetDungeon().nodes.Length - 1)
            {
                Console.WriteLine("Reached end node!");
                return true;
            }
            return false;
        }

        public int getTime()
        {
            return time;
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

        public bool Fight()
        {
            int pos = p.get_position();
            Node node = d.GetNode(pos);
            Pack pack = node.popPack();
            
            Console.WriteLine("before combat-round: "+pack.GetNumMonsters());

            p.doCombatRound(GetDungeon(), pack);

            if (!pack.isDead())
            { 
                node.pushPack(pack);
                if (CheckRetreat())
                {
                    return true;
                }
            }

            else
            {
                Console.WriteLine("Pack is killed.");
                int score = pack.get_Score();
                GivePackReward(score);
                return true;
            }
            Console.WriteLine("after combat-round: "+pack.GetNumMonsters());
            return false;
            
        }

        public bool PlayerDead()
        {
            return !p.IsAlive();
        }

        private void GivePackReward(int scr)
        {
            int score = scr;
            int nodeId = p.get_position();
            int interval = d.interval;
            if(d.GetNode(nodeId).IsBridge(interval))
            {
                // meer punten C:
                int level = nodeId / interval;
                score *= level;
                Console.WriteLine("BONUS PUNTEN! Bridge Level: " + level + " Score: " + score);

                // ITEMS GEVEN??

            }
            else
            {
                // minder punten :C
                Console.WriteLine("NORMALE PUNTEN. Score: " + score);

                // ITEMS GEVEN??
            }
            p.AwardScore(score);
        }

        public void UsePotion()
        {
            List<Item> items = p.getItems();
            for (int t =0;t<items.Count;t++)
            {
                if (items[t].type == ItemType.HealthPotion)
                { 
                    p.use(d, items[t]); items.Remove(items[t]);
                    UpdateTime();
                    break;
                }

            }
        }

        public void UseCrystal()
        {
            List<Item> items = p.getItems();
            for (int t =0;t<items.Count;t++)
            {
                if (items[t].type == ItemType.TimeCrystal)
                { 
                    p.use(d, items[t]); items.Remove(items[t]);
                    UpdateTime();
                    break;
                }
            }
        }

        public void UseScroll()
        {
            List<Item> items = p.getItems();
            for (int t = 0; t < items.Count; t++)
            {
                if (items[t].type == ItemType.MagicScroll)
                {
                    p.use(d, items[t]); items.Remove(items[t]);
                    UpdateTime();
                    break;
                }
            }
        }

        internal void UpdateTime()
        {
            time++;
            p.UpdateCurrentItem();
        }
    }
}
