using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System.Collections.Generic;
using System.Diagnostics;


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
            A_3();
            A_4();
            A_5();
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
        public void Test_B()
        {
            // constraint: “The exit is always reachable from the player’s current node.”
            B_1();
            B_2();
        }

        [TestMethod]
        public void B_1()
        {
            // default movement
            Replayer z = new Replayer("1b-1.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                GameState st = z.QueryState();
                Player p = st.GetPlayer();
                int pos = p.get_position();
                Dungeon d = st.GetDungeon();
                Stack<Node> path = d.ShortestPath(d.nodes[pos], d.nodes[d.nodes.Length-1]);
                Assert.IsTrue(path.Count > 0);
            }
        }

        [TestMethod]
        public void B_2()
        {
            // moving and causing explosion
            Replayer z = new Replayer("1b-2.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                GameState st = z.QueryState();
                Player p = st.GetPlayer();
                int pos = p.get_position();
                Dungeon d = st.GetDungeon();
                Stack<Node> path = d.ShortestPath(d.nodes[pos], d.nodes[d.nodes.Length - 1]);
                Assert.IsTrue(path.Count > 0);
            }
        }

        [TestMethod]
        public void Test_C()
        {
            // constraint: “Dropping healing potions never breaks the HP-restriction rule.”

            C_1("1c-1.txt");
            C_1("1c-2.txt");
            //C_1("1c-3.txt"); // After killing a pack?!??!
            
            // these two logs are made in one run, to show the invariant works when moving to the next-lvl
            C_1("1c-first_lvl.txt"); 
            C_1("1c-next_lvl.txt");

        }

        [TestMethod]
        private void C_1(string path)
        {
            // when there are no monsters in the game
            // no potions will be dropped when it will break the HP-property

            Replayer z = new Replayer(path);
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                GameState st = z.QueryState();
                Node[] nodes = st.GetDungeon().nodes;
                int cur_amount = 0;               // # potions in the game
                int prev_amount = 0;    // # potions before the last action
                int monsterhp = 0;
                for(int t =0;t<nodes.Length;t++)
                {
                    if (nodes[t] != null)
                    {
                        try
                        {
                            List<Item> items = nodes[t].get_Items();
                            foreach (Item i in items)
                            {
                                if (i.type == ItemType.HealthPotion)
                                    cur_amount++;
                            }
                        }
                        catch(NullReferenceException e){ Debug.WriteLine(e.Message);}

                        try{
                        Stack<Pack> packs = nodes[t].getPacks();
                        foreach(Pack p in packs)
                        {
                            if (p.GetItem().type == ItemType.HealthPotion)
                                cur_amount++;
                            monsterhp+= p.GetPackHealth();
                        }
                        }
                        catch (NullReferenceException e) { Debug.WriteLine(e.Message); }
                    }
                }

                List<Item> item = st.GetPlayer().getItems();
                foreach(Item i in item)
                {
                    if (i.type == ItemType.HealthPotion)
                        cur_amount++;
                }

                if (cur_amount > prev_amount) 
                {
                    Assert.IsTrue((st.GetPlayer().GetHP() + (cur_amount * 25)) <= monsterhp); // when # potions is changed, check if HP-property isn't broken
                    prev_amount = cur_amount; cur_amount = 0; monsterhp = 0;
                }
                
            }
        }
    }
}
