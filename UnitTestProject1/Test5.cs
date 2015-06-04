using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace UnitTestProject1
{
    [TestClass]
    public class Test5
    {
            // “Monster retreats is handled correctly (they retreat after losing 70% HP; 
            // But when they have an order, they should never retreat)”.
            //>= 30%     - No order, Hunt, Defend
            //<30%       - No order, Hunt, Defend
            //<30%       - No order but other neighbours are full
        
        [TestMethod]
        public void DefendG30()
        {
            Replayer z = new Replayer("DefG30.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(s.GetDungeon().nodes[s.GetPlayer().get_position()].hasPack());
        }

        [TestMethod]
        public void DefendL30()
        {
            Replayer z = new Replayer("DefL30.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(s.GetDungeon().nodes[s.GetPlayer().get_position()].hasPack());
        }

        [TestMethod]
        public void HuntG30()
        {
            Replayer z = new Replayer("HuntG30.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(s.GetDungeon().nodes[s.GetPlayer().get_position()].hasPack());
        }

        [TestMethod]
        public void HuntL30()
        {
            Replayer z = new Replayer("HuntL30.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(s.GetDungeon().nodes[s.GetPlayer().get_position()].hasPack());
        }

        [TestMethod]
        public void NoOrdG30()
        {
            Replayer z = new Replayer("NoOrdG30.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(s.GetDungeon().nodes[s.GetPlayer().get_position()].hasPack());
        }

        [TestMethod]
        public void NoOrdL30()
        {
            Replayer z = new Replayer("NoOrdL30.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsFalse(s.GetDungeon().nodes[s.GetPlayer().get_position()].hasPack());
        }

        [TestMethod]
        public void NoOrdL30FullNeighs()
        {
            Replayer z = new Replayer("NoOrdL30.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(s.GetDungeon().nodes[s.GetPlayer().get_position()].hasPack());
        }
    }
}
