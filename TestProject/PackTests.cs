using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{

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

            Assert.AreEqual(initial + 15, p.getInitialHP());     // check if initial HP is upped
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
