using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void TestHealthPotType()
        {
            Health_Potion hp = new Health_Potion();
            ItemType expected = ItemType.HealthPotion;
            ItemType actual = hp.type;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealthPotValue()
        {
            Health_Potion hp = new Health_Potion();
            int expected = 25;
            int actual = hp.health;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealthPotDamage()
        {
            Health_Potion hp = new Health_Potion();
            int expected = 0;
            int actual = hp.damage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealhPotDuration()
        {
            Health_Potion hp = new Health_Potion();
            int expected = 0;
            int actual = hp.duration;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealthPotString()
        {
            Health_Potion hp = new Health_Potion();
            string expected = "HealthPotion";
            string actual = hp.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestTimeCrystalType()
        {
            Time_Crystal tc = new Time_Crystal();
            ItemType exp = ItemType.TimeCrystal;
            ItemType act = tc.type;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalDuration()
        {
            Time_Crystal tc = new Time_Crystal();
            int exp = 5;
            int act = tc.duration;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalValue()
        {
            Time_Crystal tc = new Time_Crystal();
            int exp = 0;
            int act = tc.health;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalDamage()
        {
            Time_Crystal tc = new Time_Crystal();
            int exp = 0;
            int act = tc.damage;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalToString()
        {
            Time_Crystal tc = new Time_Crystal();
            string exp = "TimeCrystal";
            string act = tc.ToString();
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollType()
        {
            Magic_Scroll ms = new Magic_Scroll();
            ItemType exp = ItemType.MagicScroll;
            ItemType act = ms.type;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollValue()
        {
            Magic_Scroll ms = new Magic_Scroll();
            int exp = 0;
            int act = ms.health;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollDuration()
        {
            Magic_Scroll ms = new Magic_Scroll();
            int exp = 5;
            int act = ms.duration;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollDamage()
        {
            Magic_Scroll ms = new Magic_Scroll();
            int exp = 10;
            int act = ms.damage;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollToString()
        {
            Magic_Scroll ms = new Magic_Scroll();
            string exp = "MagicScroll";
            string act = ms.ToString();
            Assert.AreEqual(exp, act);
        }
    }
}
