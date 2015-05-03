using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project.GameState
{
    class Player
    {
        private int HPmax, HP, damage, score;
        private Item current;
        private List<Item> Items;

        public Player()
        {
            HPmax = 250;
            HP = 250;
            damage = 8;
            score = 0;
            Items = new List<Item>();
        }

        public void use(Dungeon d, Item i)
        {
            current = i;
           
            if (current.type == Item.ItemType.HealthPotion)
            {
                HP += current.health;
                if (HP > HPmax)
                    HP = HPmax;
                current = null;
            }

            //
            // Time-crystal and magic-scrol only have effect when fighting
            //

        }
        public void add(Item i)
        {
            Items.Add(i);
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
            // NEEDS IMPROVEMENTS

            // if player needs to attack
            if (current != null)
            {
                if (current.type == Item.ItemType.TimeCrystal)
                {
                    p.hit_pack_Time_Crystal_variant(damage);
                    current.duration--;
                }
                else if (current.type == Item.ItemType.MagicScroll)
                {
                    p.hit_pack(damage + current.damage);
                    current.duration--;
                }
                if (current.duration < 1)
                    current = null;
            }
            else
            {
                p.hit_pack(damage);
            }

            // if pack needs to attack player
            HP -= p.hit_player();
            if (HP <= 0)
                Console.WriteLine("Game Over");
        }
    }
}
