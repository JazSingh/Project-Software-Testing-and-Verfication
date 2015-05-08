using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void GetHP()
        {
            Player p = new Player();
            int expected = 250;
            int actual = p.GetHP();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void isAlive()
        {
            Player p = new Player();
            bool expected = true;
            bool actual = p.IsAlive();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetScore()
        {
            Player p = new Player();
            int expected = 0;
            int actual = p.getScore();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Award_Score()
        {
            AwardScore(10);
        }

        public void AwardScore(int i)
        {
            Player p = new Player();
            int score = p.getScore();
            int expected = score + i;
            p.AwardScore(i);
            int actual = p.getScore();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void set_and_getposition()
        {
            Player p = new Player();
            p.set_position(10);
            int expected = 10;
            int actual = p.get_position();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void getNRItems()
        {
            Player p = new Player();
            int actual = p.getNRPotions();
            Assert.AreEqual(0, actual);

            actual = p.getNRCrystals();
            Assert.AreEqual(0, actual);

            actual = p.getNRScrolls();
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void getItems()
        {
            Player p = new Player();
            List<Item> actual = p.getItems();
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void getPosition()
        {
            Player p = new Player();
            int actual = p.get_position();
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void setLocation()
        {
            Player p = new Player();
            p.set_position(8);
            Assert.AreEqual(8, p.get_position());
        }

        [TestMethod]
        public void AddItem()
        {
            Player p = new Player();
            p.add(new Health_Potion());
            Assert.AreEqual(1, p.getNRPotions());
            p.add(new Time_Crystal());
            Assert.AreEqual(1, p.getNRCrystals());
            p.add(new Magic_Scroll());
            Assert.AreEqual(1, p.getNRScrolls());
        }

        [TestMethod]
        public void SetupPlayerCustomized()
        {
            int hpmax = 100;
            int hp = 100;
            int damage = 10;
            int scr = 10;
            Item i = null;
            List<Item> items = new List<Item>();
            Player p = new Player(hpmax, hp, damage, scr, i, items);
            Assert.AreEqual(100, p.GetHP());
            Assert.AreEqual(10, p.getScore());
            Assert.AreEqual(0, p.getItems().Count);
        }

        [TestMethod]
        public void UseItem()
        {
            Player p = new Player();
            Dungeon d = new Dungeon(1);
            Pack pack = new Pack(8);
            p.doCombatRound(d, pack);
            p.use(d, new Health_Potion());
            Assert.AreEqual(250, p.GetHP());
            Assert.AreEqual(null, p.getCurrentItem());
        }
        
        [TestMethod]
        public void UpdateCurrentItem()
        {
            Player p = new Player();
            Dungeon d = new Dungeon(1);
            p.use(d, new Magic_Scroll());
            p.UpdateCurrentItem();
            Item i = p.getCurrentItem();
            Assert.AreEqual(ItemType.MagicScroll, i.type);
            Assert.AreEqual(5, i.duration);
        }

        [TestMethod]
        public void Save()
        {
            Player p = new Player();
            Dungeon d = new Dungeon(1);
            string filename = "test.txt";
            Assert.AreEqual(true, p.save(d, filename));
        }

        [TestMethod]
        public void Load()
        {
            Player p = new Player();
            string s = "stinrdnka";
            Assert.AreEqual(true, p.load(s));
        }
    }
}
