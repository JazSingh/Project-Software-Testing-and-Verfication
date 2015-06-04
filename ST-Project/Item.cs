using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    // enumerates the possible item types
    public enum ItemType
    {
        HealthPotion, TimeCrystal, MagicScroll
    };

    public abstract class Item
    {
        public ItemType type;
       
        // duration: how many rounds the items remains active, only for the crystal and scroll
        // health: how much the item heals, only for health potions
        // damage: how hard the item hits, only for scrolls
        public int duration, health, damage;

        // returns the item as a string, for saving
        public override string ToString()
        {
            string s = string.Empty;
            s += type.ToString("F");
            return s;
        }
    }

    public class Health_Potion : Item
    {
        // constructor for health potions
        public Health_Potion()
        {
            health = 25;
            type = ItemType.HealthPotion;
        }
    }

    public class Time_Crystal : Item
    {
        // constructor for time crystals
        public Time_Crystal()
        {
            duration = 5;
            type = ItemType.TimeCrystal;
        }
    }

    public class Magic_Scroll : Item
    {
        // constructor for magic scrolls
        public Magic_Scroll()
        {
            duration = 5;
            damage = 10;
            type = ItemType.MagicScroll;
        }
    }
}
