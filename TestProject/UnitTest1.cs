using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class MonsterTests
    {
        [TestMethod]
        public void HP_DamageTest()
        {
            HPTest();
            damageTest();
        }

        [TestMethod]
        public void GetsHitTest()
        {
            gets_hit_alive();
            get_hit_dead();
        }

        [TestMethod]
        public void ToString()
        {
            Monster m = new Monster();
            string s = string.Empty;

            s += "HP: " + 15 + Environment.NewLine;
            s += "Damage: " + 3;

            string expected = s;
            string actual = m.ToString();
            Assert.AreEqual(expected, actual);
        }

        public void gets_hit_alive()
        {
            Monster target = new Monster();
            
            bool expected = false;
            bool actual = target.gets_hit(8);
            Assert.AreEqual(expected, actual);
            
            int exp = 15 - 8;
            int act = target.GetHP();
            Assert.AreEqual(exp, act);
        }

        public void get_hit_dead()
        {
            Monster target = new Monster();

            bool expected = false;
            bool actual = target.gets_hit(8);
            Assert.AreEqual(expected, actual);

            int exp = 15 - 8;
            int act = target.GetHP();
            Assert.AreEqual(exp, act);
            
            expected = true;
            actual = target.gets_hit(8);
            Assert.AreEqual(expected, actual);
        }

        public void HPTest()
        {
            Monster m = new Monster();
            int expected = 15;
            int actual = m.GetHP();
            Assert.AreEqual(expected, actual);
        }

        public void damageTest()
        {
            Monster target = new Monster();
            int expected = 3;
            int actual = target.hits();
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class PackTests
    {

    }
}
