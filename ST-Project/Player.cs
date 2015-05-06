using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ST_Project
{
    public class Player
    {
        private int HPmax, HP, damage, score, position;
        private Item current;
        private List<Item> Items;

        public Player()
        {
            HPmax = 250;
            HP = 250;
            damage = 8;
            score = 0;
            position = 0; // ID of current node the player's in
            Items = new List<Item>();
            Items.Add(new Health_Potion());
            Items.Add(new Time_Crystal());
            Items.Add(new Magic_Scroll());
        }

        public Player(int hpmax, int hp, int dmg, int scr, Item item, List<Item> items)
        {
            // TODO: Complete member initialization
            HPmax = hpmax;
            HP = hp;
            damage = dmg;
            score = scr;
            current = item;
            Items = items;
        }

        public void use(Dungeon d, Item i)
        {
            current = i;
            current.duration++; // to neutralize the time-cost of the use (-1+1 = 0)
            if (current.type == ItemType.HealthPotion)
            {
                Console.WriteLine("Health potion genomen.");
                HP += current.health;
                if (HP > HPmax)
                    HP = HPmax;
                current = null;
            }

            //
            // Time-crystal and magic-scroll only have effect when fighting
            //
        }

        public void UpdateCurrentItem()
        {
            if (current != null)
            {
                current.duration--;
                Console.WriteLine("current.duration: " + current.duration);
                if (current.duration <= 0)
                { current = null; Console.WriteLine("Item is uitgewerkt."); }
            }
        }

        public void add(Item i)
        {
            Items.Add(i);
        }

        public bool save(Dungeon d, string filename)
        {
            string save = ToString() + Environment.NewLine + d.ToString();
            File.WriteAllText(filename, save);
            return true;
        }

        public bool load(string filename)
        {
            return true;
        }

        public int GetHP()
        {
            return HP;
        }

        public void doCombatRound(Dungeon d, Pack p)
        {
            // NEEDS IMPROVEMENTS

            // if player needs to attack
            if (current != null)
            {
                if (current.type == ItemType.TimeCrystal)
                {
                    p.hit_pack_Time_Crystal_variant(damage);
                    UpdateCurrentItem();
                }
                else if (current.type == ItemType.MagicScroll)
                {
                    p.hit_pack(damage + current.damage);
                    UpdateCurrentItem();
                }
            }

            else
            {
                if (p.hit_pack(damage) == true)
                {return;}
            }

            // if pack needs to attack player
            HP -= p.hit_player();
            if (HP <= 0)
                Console.WriteLine("Game Over");
        }

        public void AwardScore(int scr)
        {
            Console.WriteLine("Score was: " + score);
            score += scr;
            Console.WriteLine("Score is: " + score);
        }

        public override string ToString()
        {
            string s = string.Empty;
            s += "Player:" + Environment.NewLine;
            s += "HpMax: " + HPmax + Environment.NewLine;
            s += "HP: " + HP + Environment.NewLine;
            s += "Damage: " + damage + Environment.NewLine;
            s += "Score: " + score + Environment.NewLine;
            s += "Current Item: ";
            if (current == null)
                s += "none";
            else
                s += current.ToString();
            s += Environment.NewLine;
            s += "Items: " + Items.Count + Environment.NewLine;

            foreach(Item i in Items)
            {
                s += i.ToString() + Environment.NewLine;
            }

                return s;
        }

        public void set_position(int i)
        {
            position = i;
        }

        public int get_position()
        {
            return position;
        }

        public int getNRPotions()
        {
            int result = 0;
            foreach(Item i in Items)
            {
                if (i.type == ItemType.HealthPotion)
                    result++;
            }

            return result;
        }

        public int getNRCrystals()
        {
            int result = 0;
            foreach (Item i in Items)
            {
                if (i.type == ItemType.TimeCrystal)
                    result++;
            }

            return result;
        }

        public int getNRScrolls()
        {
            int result = 0;
            foreach (Item i in Items)
            {
                if (i.type == ItemType.MagicScroll)
                    result++;
            }

            return result;
        }

        public int getHealth()
        {
            return HP;
        }

        public List<Item> getItems()
        {
            return Items;
        }
    }
}
