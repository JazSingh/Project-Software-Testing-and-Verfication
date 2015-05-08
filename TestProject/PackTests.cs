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
            Pack p = new Pack(10);
            int expected = 9;
            int actual = p.get_Score();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Retreat()
        {
            Retreat_False();
            Retreat_True();
        }

        public void Retreat_False()
        {
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.retreat();
            Assert.AreEqual(expected, actual);
        }

        public void Retreat_True()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            p.hit_pack(16);
            p.hit_pack(8);
            bool expected = true;
            bool actual = p.retreat();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void isDead()
        {
            isDead_False();
            isDead_True();
            isDead_CurrentNull_False();
        }

        public void isDead_False()
        {
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.isDead();
            Assert.AreEqual(expected, actual);
        }

        public void isDead_CurrentNull_False()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            bool expected = false;
            bool actual = p.isDead();
            Assert.AreEqual(expected, actual);
        }

        public void isDead_True()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            p.hit_pack(16);
            p.hit_pack(16);
            bool expected = true;
            bool actual = p.isDead();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNumMonsters()
        {
            GetNumMonsters_NewPack();
            GetNumMonsters_NoCurrent();
        }
        public void GetNumMonsters_NewPack()
        {
            Pack p = new Pack(10);
            int expected = 3;
            int actual = p.GetNumMonsters();
            Assert.AreEqual(expected, actual);
        }

        public void GetNumMonsters_NoCurrent()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            int expected = 2;
            int actual = p.GetNumMonsters();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddMonster()
        {
            Pack p = new Pack(10);
            int initial = p.getInitialHP();
            int amount = p.GetNumMonsters();

            p.Add_Monster(new Monster());

            Assert.AreEqual(initial + 15, p.getInitialHP());     // check if initial HP is upped
            Assert.AreEqual(amount + 1, p.GetNumMonsters());  // check if #monsters is +1
        }

        [TestMethod]
        public void getInitialHP()
        {
            Pack p = new Pack(10);
            int expected = 3 * 15;
            int actual = p.getInitialHP();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPackHealth()
        {
            GetPackHealth_FreshPack();
            GetPackHealth_CurrentNull();

        }
        public void GetPackHealth_FreshPack()
        {
            Pack p = new Pack(10);
            int expected = 3 * 15;
            int actual = p.GetPackHealth();
            Assert.AreEqual(expected, actual);
        }

        public void GetPackHealth_CurrentNull()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            int expected = 2 * 15;
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
            Gets_hit_Crystal_CurrentNull_15dmg();
            Gets_hit_Crystal_CurrentNull_14dmg();
            Gets_hit_CurrentNull_NoNext();
        }

        public void Gets_hit_CurrentNull_NoNext()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            p.hit_pack(16);
            p.hit_pack(16);
            p.hit_pack(16);
        }

        public void Gets_hit_False()
        {
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.hit_pack(8);
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_CompleteFight()
        {
            Pack p = new Pack(10);
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
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(14); // attack with 14 damage
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_Crystal_True()
        {
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(15); // attack with 15 damage
            Assert.AreNotEqual(expected, actual);
        }

        public void Gets_hit_Crystal_CurrentNull_15dmg()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            bool expected = true;
            bool actual = p.hit_pack_Time_Crystal_variant(15);
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_Crystal_CurrentNull_14dmg()
        {
            Pack p = new Pack(10);
            p.hit_pack(16);
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(14);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void hit_player()
        {
            Pack p = new Pack(10);
            int expected = 3 * 3;
            int actual = p.hit_player();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetItem()
        {
            GetItem_HP();
            GetItem_MS();
            GetItem_TC();
        }

        public void GetItem_HP() 
        {
            Pack p = new Pack(1);
            string expected = "HealthPotion";
            string actual = p.GetItem().ToString();
            Assert.AreEqual(expected, actual);
        }
        public void GetItem_TC() 
        {
            Pack p = new Pack(4);
            string expected = "TimeCrystal";
            string actual = p.GetItem().ToString();
            Assert.AreEqual(expected, actual);
        }
        public void GetItem_MS() 
        {
            Pack p = new Pack(7);
            string expected = "MagicScroll";
            string actual = p.GetItem().ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
