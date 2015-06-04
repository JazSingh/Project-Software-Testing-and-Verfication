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
        public void Test_A()
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

            //KIJKEN OF REPLAY WERKT!
            Replayer z = new Replayer("test.txt");
            z.Init();
            while (z.HasNext())
            {
               z.Step();
            }
            Dungeon d = z.QueryState().GetDungeon();
            Node[] nodes = d.nodes;
            //for (int t = 0; t < nodes.Length; t++)
            //{
            //    Stack<Pack> packs = nodes[t].getPacks();
            //    int cap = 0;
            //    foreach (Pack p in packs)
            //        cap += p.GetNumMonsters();
            //    Assert.IsTrue(cap <= nodes[t].GetCapacity()); 
            //}
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
