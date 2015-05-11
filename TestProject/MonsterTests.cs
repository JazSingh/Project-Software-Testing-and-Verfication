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
            // tests method GetHP() and hits() of monster
            HPTest();
            damageTest(); 
        }

        public void HPTest()
        {
            // tests if a new Monster-object has 15 HP
            Monster m = new Monster();
            int expected = 15;
            int actual = m.GetHP();
            Assert.AreEqual(expected, actual);
        }

        public void damageTest()
        {
            // tests if a new Monster has as damage 3
            Monster target = new Monster();
            int expected = 3;
            int actual = target.hits();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetsHitTest()
        {
            gets_hit_alive();
            get_hit_dead();
        }


        public void gets_hit_alive()
        {
            // pre: new Monster, full health
            // post: monster is hit once, still alive
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
            // pre: monster is alive
            // post: monster is killed after being hit
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

        [TestMethod]
        public void TToString()
        {
            // tests the ToString-method of monster
            Monster m = new Monster();
            string s = string.Empty;

            s += "HP: " + 15 + Environment.NewLine;
            s += "Damage: " + 3;

            string expected = s;
            string actual = m.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
