using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project.GameState
{
    class Pack
    {
        private Stack<Monster> monsters; // group of monsters who form the pack
        private Monster current; // monster who will get damaged when the player attacks the pack
        private Item item;
        private int score;

        public Pack(int i)
        {
            monsters = new Stack<Monster>();

            // NEEDS IMPROVEMENTS!!!
            score = i; 
            Random r = new Random();
            int val = r.Next(0, 19);
            if (val < 3)
                item = new Health_Potion(100);
            else if (val > 2 && val < 6)
                item = new Time_Crystal(10);
            else if (val > 5 && val < 9)
                item = new Magic_Scroll(10, 20);
            //////////////////////////////////////
        }

        public int get_Score()
        {
            return score;
        }

        public void Add_Monster(Monster i)
        {
            monsters.Push(i);
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
                    return true; // pack is dead (each monster in this pack died)
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
    }
}
