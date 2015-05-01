using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project.GameState
{
    abstract class Item
    {
        public int duration, health, damage;
        public bool health_potion, time_crystal, magic_scroll;
    }

    public class Healt_Potion : Item
    {
        public Healt_Potion(int i)
        {
            health = i;
            health_potion = true;
        }
    }

    class Time_Crystal : Item
    {
        public Time_Crystal(int i)
        {
            duration = i;
            time_crystal = true;
        }
    }

    class Magic_Scroll : Item
    {
        public Magic_Scroll(int dur, int dam)
        {
            duration = dur;
            damage = dam;
            magic_scroll = true;
        }
    }
}
