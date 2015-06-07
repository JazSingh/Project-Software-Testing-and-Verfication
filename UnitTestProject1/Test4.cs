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
            Assert.AreEqual(4, s.GetPlayer().get_position());
        }

        [TestMethod]
        public void MagicScrollAttack() 
        {
            Replayer z = new Replayer("ScrollAttack.txt");
            z.Init();
            z.Step();
            int inithp = z.QueryState().GetDungeon().nodes[0].getPacks().Peek().GetPackHealth();
            z.Step();
            int dmg1 = inithp - z.QueryState().GetDungeon().nodes[0].getPacks().Peek().GetPackHealth();
            z.Step();
            z.Step();
            int dmg2 = (inithp + dmg1) - z.QueryState().GetDungeon().nodes[0].getPacks().Peek().GetPackHealth();

            Assert.IsTrue(dmg1 < dmg2);
        }

        [TestMethod]
        public void MagicScrollKill() 
        {
            Replayer z = new Replayer("ScrollKill.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.AreEqual(0, s.GetDungeon().SumMonsterHealth());
        }

        [TestMethod]
        public void CrystalAttack()
        {
            Replayer z = new Replayer("CrystalAttack.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            foreach (Monster m in s.GetDungeon().nodes[0].getPacks().Pop().getMonsters()) Assert.AreEqual(7, m.GetHP());
        }

        [TestMethod]
        public void CrystalKill()
        {
            Replayer z = new Replayer("CrystalKill.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.AreEqual(0, s.GetDungeon().SumMonsterHealth());
        }

        [TestMethod]
        public void ItemOverwrite()
        {
            Replayer z = new Replayer("ItemOverwrite.txt");
            z.Init();
            while (z.HasNext())
                z.Step();
            GameState s = z.QueryState();
            Assert.AreEqual("TimeCrystal",s.GetPlayer().getCurrentItem().ToString());
        }

        [TestMethod]
        public void ItemNextLevel()
        {
            Replayer z = new Replayer("Itemnlvl1.txt");
            z.Init();
            while (z.HasNext())
                z.Step();

            Replayer zn = new Replayer("Itemnlvl2.txt");
            zn.Init();
            while (zn.HasNext())
                zn.Step();
            GameState s2 = zn.QueryState();
            GameState s = z.QueryState();
            Assert.AreEqual(s.GetPlayer().getCurrentItem().ToString(), s2.GetPlayer().getCurrentItem().ToString());
        }

    }
}
