using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System.Collections.Generic;


namespace TestProject
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        void Test_A()
        {
            // constraint: “The node-capacity rule is never breached.”
            // use MBC, with base: (full, yes, 2)

            /*
              Pseudo-code
              Log z = Load(...);
              z.init();
              while (z.hasNext()) 
              {
                GameState q = z.queryState() ;
                foreach Node n
                    Assert... n.capacity <= max_capacity
                z.next();
              }
             */
        }

        [TestMethod]
        void Test_B()
        {
            // constraint: “The exit is always reachable from the player’s current node.”
        }

        [TestMethod]
        void Test_C()
        {
            // constraint: “Dropping healing potions never breaks the HP-restriction rule.”
        }
    }
}
