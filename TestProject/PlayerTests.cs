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
            // tests if player returns it's current HP correctly
            Player p = new Player();
            int expected = 250;
            int actual = p.GetHP();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void isAlive()
        {
            // tests if player returns it's life-status correctly
            Player p = new Player();
            bool expected = true;
            bool actual = p.IsAlive();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetScore()
        {
            // tests if player returns it's score correctly
            Player p = new Player();
            int expected = 0;
            int actual = p.getScore();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Award_Score()
        {
            // tests if player processes an award correctly
            AwardScore(10);
        }

        public void AwardScore(int i)
        {
            // tests if player processes an award correctly
            Player p = new Player();
            int score = p.getScore();
            int expected = score + i;
            p.AwardScore(i);
            int actual = p.getScore();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void getNRItems()
        {
            // tests if player returns it's number of items (inventory) correctly
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
            // tests if player returns it's items (inventory) correctly
            Player p = new Player();
            List<Item> actual = p.getItems();
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void getPosition()
        {
            // tests if player returns it's position correctly
            Player p = new Player();
            int actual = p.get_position();
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void setPosition()
        {
            // tests if player sets it's position correctly
            Player p = new Player();
            p.set_position(8);
            Assert.AreEqual(8, p.get_position());
        }

        [TestMethod]
        public void AddItem()
        {
            // tests if player adds an Item to it's inventory correctly
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
            // tests if the load-constructor-method works correctly
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
        public void UseHealthPotion()
        {
            // tets if a player uses a Health Potion correctly
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
            // tests if player updates it's current item correctly
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
            // tests if the save-method of Player works correctly
            Player p = new Player();
            Dungeon d = new Dungeon(1);
            string filename = "test.txt";
            Assert.AreEqual(true, p.save(d, filename));
        }

        [TestMethod]
        public void Load()
        {
            // tests if the load-method of Player works correctly
            Player p = new Player();
            string s = "stinrdnka";
            Assert.AreEqual(true, p.load(s));
        }

        [TestMethod]
        public void ScrollActive()
        {
            // tests if a player uses a Magic Scroll correctly
            Player p = new Player();
            Dungeon d = new Dungeon(5);
            Pack pa = new Pack(0);
            p.use(d, new Magic_Scroll());

            p.doCombatRound(d, pa);
            //Assert.IsTrue(p.getCurrentItem().type == ItemType.MagicScroll);
            Assert.AreEqual(pa.GetPackHealth(), 30);
        }

        [TestMethod]
        public void CrystalActive()
        {
            // tests if a player uses a Time Crystal correctly
            Player p = new Player();
            Dungeon d = new Dungeon(5);
            Pack pa = new Pack(0);
            p.use(d, new Time_Crystal());

            p.doCombatRound(d, pa);
            Assert.AreEqual(pa.GetPackHealth(), 21);
        }

        [TestMethod]
        public void ItemExpire()
        {
            // tests if an item is removed correctly, after it's consumed
            Player p = new Player();
            Dungeon d = new Dungeon(5);
            Pack pa = new Pack(0);
            Time_Crystal tc = new Time_Crystal();
            p.use(d, tc);
            tc.duration = 0;
            p.UpdateCurrentItem();
            Assert.IsNull(p.getCurrentItem());
        }

        [TestMethod]
        public void StringWithItems()
        {   // tests if save-methods used for saving the players' items works correctly
            Player p = new Player();
            p.add(new Health_Potion());
            p.add(new Time_Crystal());
            p.add(new Magic_Scroll());
            p.use(new Dungeon(1), new Time_Crystal());
            string s = p.ToString();
            Assert.IsTrue(s.Contains(new Health_Potion().ToString()) && s.Contains(new Time_Crystal().ToString()) && s.Contains(new Magic_Scroll().ToString()));
        }

        [TestMethod]
        public void AttackPack()
        {
            Player p = new Player();
            p.set_position(0);
            Node n = new Node(0);

        }
    }
}
