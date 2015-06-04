using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class Test3
    {
        // 2. Orders (Hunt and Defend) are handled correctly:

        [TestMethod]
        public void Test_A()
        {
            // constraint: "Orders are indeed issued when situations that should trigger them occur."

            A_Hunt();
            A_Defend();
        }

        [TestMethod]
        public void A_Defend()
        {
            Defend_NoOrders("3a-1.txt"); // test constraint when moving and using a Health Potion/Magic Scroll
            Defend_NoOrders("3a-2.txt"); // test constraint when fighting
            Defend_Orders("3a-4.txt");   // test constraint after using a Time Crystal
        }

        public void Defend_Orders(string p)
        {
            Replayer z = new Replayer(p);
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            GameState st = z.QueryState();
            Assert.IsTrue(st.GetDungeon().GetNumDefendOrders() > 0);
        }

        public void Defend_NoOrders(string p)
        {
            Replayer z = new Replayer(p);
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                GameState st = z.QueryState();
                Assert.IsTrue(st.GetDungeon().GetNumDefendOrders() == 0);
            }
        }

        [TestMethod]
        public void A_Hunt()
        {
            Hunt_NoOrders("3a-1.txt"); // test constraint when moving and using a Health Potion/Magic Scroll
            Hunt_NoOrders("3a-2.txt"); // test constraint when fighting
            Hunt_Orders("3a-3.txt");   // test constraint after using a Time Crystal
        }

        public void Hunt_NoOrders(string p)
        {
            Replayer z = new Replayer(p);
            z.Init();
            while (z.HasNext())
            {
                z.Step();
                GameState st = z.QueryState();
                Assert.IsTrue(st.GetDungeon().GetNumHuntOrders() == 0);
            }
        }

        public void Hunt_Orders(string p)
        {
            Replayer z = new Replayer(p);
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }

            GameState st = z.QueryState();
            Assert.IsTrue(st.GetDungeon().GetNumHuntOrders() > 0);
        }

        [TestMethod]
        public void Test_B()
        {
            // constraint: “If there is no situation that prevents a monster pack 
            // to finish its order (it didn’t eliminated along the way, it is not 
            // continously blocked by a full node), then it will reach its destination node, 
            // as specified by the order.”
        }
    }
}
