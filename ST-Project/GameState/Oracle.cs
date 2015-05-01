using System;

namespace ST_Project.GameState
{
    static class Oracle
    {
        //Pre: -
        //Post: A random true or false output.
        public static bool Decide()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(0, 2) == 1;
        }

        //Pre: min <= max
        //Post: A random integer in the range [min, max]
        public static int GiveNumber(int min, int max)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(min, max + 1);
        }

        //Pre: max >= 0
        //Post: A random integer in the range [0, max]
        public static int GiveNumber(int max)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(0, max + 1);
        }
    }
}