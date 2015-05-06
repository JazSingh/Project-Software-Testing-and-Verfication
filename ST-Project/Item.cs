using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    public enum ItemType
    {
        HealthPotion, TimeCrystal, MagicScroll
    };

   public abstract class Item
    {
        public ItemType type;

        public int duration, health, damage;


        public override string ToString()
        {
            string s = string.Empty;
            s += "Type: " + type.ToString("F") + Environment.NewLine;
            return s;
        }
        
    }

   public class Health_Potion : Item
    {
        public Health_Potion()
        {
            health = 25;
            type = ItemType.HealthPotion;
        }
    }

    public class Time_Crystal : Item
    {
        public Time_Crystal()
        {
            duration = 5;
            type = ItemType.TimeCrystal;
        }
    }

    public class Magic_Scroll : Item
    {
        public Magic_Scroll()
        {
            duration = 5;
            damage = 10;
            type = ItemType.MagicScroll;
        }
    }
}
