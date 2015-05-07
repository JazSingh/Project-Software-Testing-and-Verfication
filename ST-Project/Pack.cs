using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    public class Pack
    {
        private Stack<Monster> monsters; // group of monsters who form the pack
        private Monster current; // monster who will get damaged when the player attacks the pack
        private Item item;
        private int score, init_hp;

        private const int packSize = 3;

        public Pack()
        {
            monsters = new Stack<Monster>();

            monsters.Push(new Monster());
            monsters.Push(new Monster());
            monsters.Push(new Monster());
            init_hp = 3 * 15;
            // NEEDS IMPROVEMENTS!!!
            score = 9; 
            Random r = new Random();
            int val = r.Next(0, 19);
            if (val < 3)
                item = new Health_Potion();
            else if (val > 2 && val < 6)
                item = new Time_Crystal();
            else if (val > 5 && val < 9)
                item = new Magic_Scroll();
            //////////////////////////////////////
        }

        public int get_Score()
        {
            return score;
        }
        
        public bool retreat()
        {
            int current_hp = 0;
            if (current != null)
                current_hp += current.GetHP();
            foreach (Monster m in monsters)
                current_hp += m.GetHP();

            if (current_hp < 0.3 * init_hp)
            {
                init_hp = current_hp; return true;
            }
            return false;
        }

        public bool isDead()
        {
            if (current == null && monsters.Count == 0)
                return true;
            return false;
        }

        public int GetNumMonsters()
        {
            return current == null? monsters.Count : (monsters.Count + 1);
        }

        public void Add_Monster(Monster i)
        {
            monsters.Push(i);
            init_hp += i.GetHP();
        }

        public int getInitialHP()
        {
            return init_hp;
        }

        public int GetPackHealth()
        {
            int sum = current == null ? 0 : current.GetHP();
            foreach (Monster m in monsters)
            {
               sum += m.GetHP();
            }

            return sum;
        }

        // realizes damage dealt by the player and
        // returns boolean value:
        // true if the pack died
        // false if the pack is still alive
        public bool hit_pack(int i)
        {
            if (current == null)
            {
                current = monsters.Pop();
            }

            bool dead = current.gets_hit(i);
            if (dead)
            {
                if (monsters.Count() > 0)
                    current = monsters.Pop();
                else
                {
                    current = null;
                    return true; // pack is dead (each monster in this pack died)
                }
            }
            return false; // pack is still alive
        }

        // Time Crystal-variant of hitting a pack.
        public bool hit_pack_Time_Crystal_variant(int i)
        {
            Stack<Monster> survivors = new Stack<Monster>();

            if (current != null)
            {
                if(!current.gets_hit(i))
                    survivors.Push(current);
                current = null;
            }

            foreach(Monster m in monsters)
            {
               if (!m.gets_hit(i))
               {
                   survivors.Push(m);
               }
            }

            if (survivors.Count > 0)
            {
                monsters.Clear();
                foreach (Monster m in survivors)
                {
                    monsters.Push(m);
                }
                return false;
            }
            else
                monsters.Clear();
            return true;
        }

        // returns total damage dealt by the pack
        public int hit_player()
        {
            int total = 0;
            if (current != null)
                total += current.hits();
            foreach (Monster m in monsters)
                total += m.hits();
            return total;
        }

        //public override string ToString()
        //{
        //    string s = string.Empty;
        //    s += "Score: " + score + Environment.NewLine;
        //    s += "Item: " + (item == null ? "" : item.ToString()) + Environment.NewLine;
        //    s += "Current: " + (current == null ? "" : current.ToString()) + Environment.NewLine;
        //    s += "Monsters: " + Environment.NewLine;

        //    foreach (Monster m in monsters)
        //    {
        //        s += m.ToString() + Environment.NewLine;
        //    }

        //    return s;
        //}
    }
}
