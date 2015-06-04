using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace UnitTestProject1
{
    [TestClass]
    public class Test4
    {
            // constraint: “When the player uses an item, the correct effect is fired. 
            // This includes the destruction of nodes, which the use of a Magic Scroll may trigger.”

        [TestMethod]
        public void HealthPotFullHP() 
        {
            Replayer z = new Replayer("HealthPotFullHP.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.AreEqual(250, s.GetPlayer().GetHP());
        }

        [TestMethod]
        public void HealtPotNoExceedMax() 
        {
            Replayer z = new Replayer("HealthPotNoExceedMax.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.AreEqual(250, s.GetPlayer().GetHP());
        }

        [TestMethod]
        public void HealthPot() 
        {
            Replayer z = new Replayer("HealthPotWorks.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.AreEqual(175, s.GetPlayer().GetHP());
        }

        [TestMethod]
        public void MagicScrollBoom() 
        {
            Replayer z = new Replayer("ScrollBoom.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(0 != s.GetPlayer().get_position());
        }

        [TestMethod]
        public void MagicScrollAttack() 
        {
            Replayer z = new Replayer("ScrollAttack.txt");
            z.Init();
            GameState s = z.QueryState();
            z.Step();
            int dmg1 = z.QueryState().GetDungeon().nodes[0].getPacks().Peek().GetPackHealth();
            z.Step();
            z.Step();
            z.Step();
            z.Step();
            z.Step();
            int dmg2 = z.QueryState().GetDungeon().nodes[0].getPacks().Peek().GetPackHealth();

            Assert.IsTrue(dmg < dmg1);
        }

        [TestMethod]
        public void MagicScrollKill() 
        {
            Replayer z = new Replayer("ScrollKill.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(0 != s.GetPlayer().get_position());
        }

        [TestMethod]
        public void CrystalAttack()
        {
            Replayer z = new Replayer("CrystalAttack.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(0 != s.GetPlayer().get_position());
        }

        [TestMethod]
        public void CrystalKill()
        {
            Replayer z = new Replayer("CrystalKill.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(0 != s.GetPlayer().get_position());
        }

        [TestMethod]
        public void ItemOverwrite()
        {
            Replayer z = new Replayer("ItemOverwrite.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(0 != s.GetPlayer().get_position());
        }

        [TestMethod]
        public void ItemNextLevel()
        {
            Replayer z = new Replayer("Itemnlvl1.txt");
            Replayer zn = new Replayer("Itemnlvl2.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.IsTrue(0 != s.GetPlayer().get_position());
        }

    }
}
