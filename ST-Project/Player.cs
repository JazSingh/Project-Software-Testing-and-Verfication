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
        private int HPmax, HP, damage, score, position; // obvious ints
        private Item current; // current item of the player
        private List<Item> Items; // items in players' inventory
        private int LKP; // Last-Known-Position

        // constructor
        public Player()
        {
            HPmax = 250;
            HP = 250;
            damage = 8;
            score = 0;
            position = 0; // ID of current node the player's in
            LKP = 0;
            Items = new List<Item>();
        }

        // constructor used to load a save-file
        public Player(int hpmax, int hp, int dmg, int scr, Item item, List<Item> items)
        {
            HPmax = hpmax;
            HP = hp;
            damage = dmg;
            score = scr;
            current = item;
            Items = items;
            position = 0;
            LKP = 0;
        }

        public int getLKP()
        {
            return LKP;
        }

        public void setLKP(int x)
        {
            LKP = x;
        }

        // player uses a certain item i
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
        }

        // update current item (decreases duration)
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

        // returns current item
        public Item getCurrentItem()
        {
            return current;
        }

        // adds an item to players' inventory
        public void add(Item i)
        {
            Items.Add(i);
        }

        // saves player in a certain save-file
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

        // returns current HP
        public int GetHP()
        {
            return HP;
        }

        // returns true when player is alive, else false
        public bool IsAlive()
        {
            return HP > 0;
        }

        // method realizes a combat-round in a battle, 
        // between the player and a certain pack.
        // first the player hits the pack,
        // then (if the pack is still alive) the pack 
        // attacks the player.
        public void doCombatRound(Dungeon d, Pack p)
        {
            // if player uses an item at the moment
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

            // if player doesn't use an item
            else
            {
                if (p.hit_pack(damage) == true)
                {
                    Item i = p.GetItem();
                    if (i != null)
                    {
                        Console.WriteLine("Item gekregen van verslagen pack!");
                        add(i);
                    }
                    return;
                }
            }

            // pack attacks player
            HP -= p.hit_player();
        }

        // players' score gets increases by integer "scr"
        public void AwardScore(int scr)
        {
            Console.WriteLine("Score was: " + score);
            score += scr;
            Console.WriteLine("Score is: " + score);
        }

        // method used for saving
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

            int mscount = 0;
            int hpcount = 0;
            int tccount = 0;
            foreach (Item i in Items)
            {
                if (i.ToString() == "TimeCrystal")
                    tccount++;
                if (i.ToString() == "HealthPotion")
                    hpcount++;
                if (i.ToString() == "MagicScroll")
                    mscount++;
            }
               

            s += "HealthPotions " + hpcount + Environment.NewLine;
            s += "TimeCrystals " + tccount + Environment.NewLine;
            s += "MagicScrolls " + mscount + Environment.NewLine;
            
            return s;
        }

        // sets current position of player
        public void set_position(int i)
        {
            position = i;
        }

        // gets current position of player
        public int get_position()
        {
            return position;
        }

        // sets current HP of player
        public void set_HP(int i)
        {
            HP = i;
        }

        // gets number of potions inside the players' inventory
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

        // gets number of Time Crystals inside the players' inventory
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

        // gets number of Magic Scrolls inside the players' inventory
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

        // returns item in inventory
        public List<Item> getItems()
        {
            return Items;
        }

        // returns score
        public int getScore()
        {
            return score;
        }
    }
}
