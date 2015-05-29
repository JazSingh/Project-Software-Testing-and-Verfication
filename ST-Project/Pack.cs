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
        private Monster current = null; // monster who will get damaged when the player attacks the pack
        private Item item = null;       // possible item the pack drops when killed
        private int score, init_hp;
        private bool isMoved = false; // boolean used for moving the pack around in the Dungeon
        private bool hunt = false;

        private const int packSize = 3;

        // constructor
        public Pack(int val)
        {
            monsters = new Stack<Monster>();

            monsters.Push(new Monster());
            monsters.Push(new Monster());
            monsters.Push(new Monster());
            init_hp = 3 * 15;
            score = 9; 
            if (val < 3)
                item = new Health_Potion();
            if (val >= 3 && val < 6)
                item = new Time_Crystal();
            if (val >= 6 && val < 9)
                item = new Magic_Scroll();
        }

        //Test Constructor
        public Pack(Stack<Monster> mons)
        {
            monsters = mons;
            score = 9;
            init_hp = mons.Count * 15;
            item = new Health_Potion();
        }

        // returns current score
        public int get_Score()
        {
            return score;
        }

        // sets hunt-boolean to true os the pack will hunt the player
        public void GoHunt()
        {
            hunt = true;
        }

        // sets hunt-boolean to true os the pack won't hunt the player anymore
        public void StopHunt()
        {
            hunt = false;
        }
        
        // returns if pack needs to retreat, based on rule of >= 70% hp-drop
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

        // returns true if pack is killed
        public bool isDead()
        {
            if (current == null && monsters.Count == 0)
                return true;
            return false;
        }

        // returns amount of monsters in the pack
        public int GetNumMonsters()
        {
            return current == null? monsters.Count : (monsters.Count + 1);
        }

        // adds monster to pack
        public void Add_Monster(Monster i)
        {
            monsters.Push(i);
            init_hp += i.GetHP();
        }

        // returns initial HP
        public int getInitialHP()
        {
            return init_hp;
        }

        // returns total HP of pack
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
                try { current = monsters.Pop(); }
                catch { return true; }
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

        public Monster getCurrent()
        {
            return current;
        }
        public Stack<Monster> getMonsters()
        {
            return monsters;
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

        // returns possible Item of pack
        public Item GetItem()
        {
            return item;
        }

        // sets boolean isMoved to a certain boolean t
        public void Moved(bool t)
        {
            isMoved = t;
        }

        // returns boolean isMoved
        public bool is_Moved()
        {
            return isMoved;
        }
    }
}
