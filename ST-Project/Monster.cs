using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    public class Monster
    {
       private int HP;
       private int damage;

       public Monster()
       {
           HP = 15;          // health-points
           damage = 3;      // damage dealt by monster when hitting player
       }

       // decreases it's current HP and returns a boolean value:
       // true if the monster is killed.
       // false if the monster is still alive.
       public bool gets_hit(int i)
       {
           HP -= i;
           if (HP <= 0)
               return true;
           return false;
       }

       // returns damage-points the monster can deal each round it attacks
       public int hits()
       {
           return damage;
       }

       public int GetHP()
       {
           return HP;
       }

       public override string ToString()
       {
           string s = string.Empty;

           s += "HP: " + HP + Environment.NewLine;
           s += "Damage: " + damage;

           return s;
       }
    }
}
