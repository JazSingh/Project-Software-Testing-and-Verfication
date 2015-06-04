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

            A_1();
            A_2();
        }

        [TestMethod]
        public void A_1()
        {
            // (Full-Yes-2)
            Replayer z = new Replayer("1a-1.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                Dungeon d = z.QueryState().GetDungeon();
                Node[] nodes = d.nodes;
                for (int t = 0; t < nodes.Length; t++)
                {
                    if (nodes[t] != null)
                    {
                        Stack<Pack> packs = nodes[t].getPacks();
                        int cap = 0;
                        foreach (Pack p in packs)
                            cap += p.GetNumMonsters();
                        Assert.IsTrue(cap <= nodes[t].GetCapacity());
                    }
                }
            }
            
            
        }

        [TestMethod]
        public void A_2()
        {
            // (NotFull-Yes-2)
            Replayer z = new Replayer("1a-2.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step(); 
                Dungeon d = z.QueryState().GetDungeon();
                Node[] nodes = d.nodes;
                for (int t = 0; t < nodes.Length; t++)
                {
                    if (nodes[t] != null)
                    {
                        Stack<Pack> packs = nodes[t].getPacks();
                        int cap = 0;
                        foreach (Pack p in packs)
                            cap += p.GetNumMonsters();
                        Assert.IsTrue(cap <= nodes[t].GetCapacity());
                    }
                }
            }
        }

        [TestMethod]
        public void A_3()
        {
            // (Full-No-2)
            Replayer z = new Replayer("1a-3.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                Dungeon d = z.QueryState().GetDungeon();
                Node[] nodes = d.nodes;
                for (int t = 0; t < nodes.Length; t++)
                {
                    if (nodes[t] != null)
                    {
                        Stack<Pack> packs = nodes[t].getPacks();
                        int cap = 0;
                        foreach (Pack p in packs)
                            cap += p.GetNumMonsters();
                        Assert.IsTrue(cap <= nodes[t].GetCapacity());
                    }
                }
            }
        }

        [TestMethod]
        public void A_4()
        {
            // (Full-Yes-3)
            Replayer z = new Replayer("1a-4.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                Dungeon d = z.QueryState().GetDungeon();
                Node[] nodes = d.nodes;
                for (int t = 0; t < nodes.Length; t++)
                {
                    if (nodes[t] != null)
                    {
                        Stack<Pack> packs = nodes[t].getPacks();
                        int cap = 0;
                        foreach (Pack p in packs)
                            cap += p.GetNumMonsters();
                        Assert.IsTrue(cap <= nodes[t].GetCapacity());
                    }
                }
            }
        }

        [TestMethod]
        public void A_5()
        {
            // (Full-Yes-1)
            Replayer z = new Replayer("1a-5.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                Dungeon d = z.QueryState().GetDungeon();
                Node[] nodes = d.nodes;
                for (int t = 0; t < nodes.Length; t++)
                {
                    if (nodes[t] != null)
                    {
                        Stack<Pack> packs = nodes[t].getPacks();
                        int cap = 0;
                        foreach (Pack p in packs)
                            cap += p.GetNumMonsters();
                        Assert.IsTrue(cap <= nodes[t].GetCapacity());
                    }
                }
            }
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
