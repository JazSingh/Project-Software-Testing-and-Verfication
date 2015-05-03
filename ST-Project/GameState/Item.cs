﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project.GameState
{


    abstract class Item
    {
        public enum ItemType
        {
            HealthPotion, TimeCrystal, MagicScroll
        };

        public ItemType type;

        public int duration, health, damage;
        
    }

    class Health_Potion : Item
    {
        public Health_Potion(int i)
        {
            health = i;
            type = ItemType.HealthPotion;
        }
    }

    class Time_Crystal : Item
    {
        public Time_Crystal(int i)
        {
            duration = i;
            type = ItemType.TimeCrystal;
        }
    }

    class Magic_Scroll : Item
    {
        public Magic_Scroll(int dur, int dam)
        {
            duration = dur;
            damage = dam;
            type = ItemType.MagicScroll;
        }
    }
}
