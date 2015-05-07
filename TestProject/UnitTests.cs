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
        [TestMethod]
        public void ScoreTest()
        {
            Pack p = new Pack();
            int expected = 9;
            int actual = p.get_Score();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Retreat()
        {
            Retreat_False();
        }

        public void Retreat_False()
        {
            Pack p = new Pack();
            bool expected = false;
            bool actual = p.retreat();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void isDead_False()
        {
            Pack p = new Pack();
            bool expected = false;
            bool actual = p.isDead();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void getNumMonsters()
        {
            Pack p = new Pack();
            int expected = 3;
            int actual = p.GetNumMonsters();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddMonster()
        {
            Pack p = new Pack();
            int initial = p.getInitialHP();
            int amount = p.GetNumMonsters();

            p.Add_Monster(new Monster());

            Assert.AreEqual(initial+15, p.getInitialHP());     // check if initial HP is upped
            Assert.AreEqual(amount + 1, p.GetNumMonsters());  // check if #monsters is +1
        }

        [TestMethod]
        public void getInitialHP()
        {
            Pack p = new Pack();
            int expected = 3 * 15;
            int actual = p.getInitialHP();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPackHealth()
        {
            Pack p = new Pack();
            int expected = 3 * 15;
            int actual = p.GetPackHealth();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Gets_Hit()
        {
            Gets_hit_False();
            Gets_hit_CompleteFight();
            Gets_hit_Crystal_False();
            Gets_hit_Crystal_True();
        }

        public void Gets_hit_False()
        {
            Pack p = new Pack();
            bool expected = false;
            bool actual = p.hit_pack(8);
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_CompleteFight()
        {
            Pack p = new Pack();
            bool expected = false;
            bool actual = false;
            for (int t = 0; t < 2; t++)
            {
                actual = p.hit_pack(15);
                Assert.AreEqual(expected, actual);
            }
            actual = p.hit_pack(15);
            Assert.AreNotEqual(expected, actual);
        }

        public void Gets_hit_Crystal_False()
        {
            Pack p = new Pack();
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(14); // attack with 14 damage
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_Crystal_True()
        {
            Pack p = new Pack();
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(15); // attack with 15 damage
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void hit_player()
        {
            Pack p = new Pack();
            int expected = 3 * 3;
            int actual = p.hit_player();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString()
        {
            // ??????
        }
    }
}
