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
            // test if the health potion has the right type
            Health_Potion hp = new Health_Potion();
            ItemType expected = ItemType.HealthPotion;
            ItemType actual = hp.type;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealthPotValue()
        {
            // check if the health potion heals for the right amount of hp
            Health_Potion hp = new Health_Potion();
            int expected = 25;
            int actual = hp.health;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealthPotDamage()
        {
            // test if the health potion does no damage
            Health_Potion hp = new Health_Potion();
            int expected = 0;
            int actual = hp.damage;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealhPotDuration()
        {
            // test if the health potion is instant; duration == 0
            Health_Potion hp = new Health_Potion();
            int expected = 0;
            int actual = hp.duration;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHealthPotString()
        {
            // test if the ToString returns the right value
            Health_Potion hp = new Health_Potion();
            string expected = "HealthPotion";
            string actual = hp.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestTimeCrystalType()
        {
            //test if the TimeCrystal has the right value
            Time_Crystal tc = new Time_Crystal();
            ItemType exp = ItemType.TimeCrystal;
            ItemType act = tc.type;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalDuration()
        {
            // test if the time crystal has a duration of 5 rounds
            Time_Crystal tc = new Time_Crystal();
            int exp = 5;
            int act = tc.duration;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalValue()
        {
            // test if the time does not have any health value
            Time_Crystal tc = new Time_Crystal();
            int exp = 0;
            int act = tc.health;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalDamage()
        {
            // test if the time crystal cannot do any damage
            Time_Crystal tc = new Time_Crystal();
            int exp = 0;
            int act = tc.damage;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestTimeCrystalToString()
        {
            // test if the time crystal ToString returns the right value
            Time_Crystal tc = new Time_Crystal();
            string exp = "TimeCrystal";
            string act = tc.ToString();
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollType()
        {
            // test if the magic scroll has the right type
            Magic_Scroll ms = new Magic_Scroll();
            ItemType exp = ItemType.MagicScroll;
            ItemType act = ms.type;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollValue()
        {
            // test if the magic scroll does not have any hp value
            Magic_Scroll ms = new Magic_Scroll();
            int exp = 0;
            int act = ms.health;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollDuration()
        {
            // test if magic scrolls have a duration of 5 rounds
            Magic_Scroll ms = new Magic_Scroll();
            int exp = 5;
            int act = ms.duration;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollDamage()
        {
            // test if magic scrolls can deal damage
            Magic_Scroll ms = new Magic_Scroll();
            int exp = 10;
            int act = ms.damage;
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void TestMagicScrollToString()
        {
            // test if  the magic scroll ToString returns the right value
            Magic_Scroll ms = new Magic_Scroll();
            string exp = "MagicScroll";
            string act = ms.ToString();
            Assert.AreEqual(exp, act);
        }
    }
}
