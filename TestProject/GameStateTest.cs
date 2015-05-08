using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class GameStateTest
    {
        [TestMethod]
        public void GetDungeon()
        {
            Dungeon d = new Dungeon(1);
            Player p = new Player();
            GameState gst = new GameState(d, p);

            Dungeon d2 = gst.GetDungeon();
            bool expected = true;
            bool actual = true;
            if (d.GetHashCode() != d2.GetHashCode())
                actual = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPlayer()
        {
            Dungeon d = new Dungeon(1);
            Player p = new Player();
            GameState gst = new GameState(d, p);

            Player p2 = gst.GetPlayer();
            bool expected = true;
            bool actual = true;
            if (p.GetHashCode() != p2.GetHashCode())
                actual = false;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NextLevel()
        {
            NextLevel_5();
            NextLevel_Not5();
        }

        private void NextLevel_Not5()
        {
            Dungeon d = new Dungeon(4);
            Player p = new Player();
            GameState gst = new GameState(d, p);
            gst.NextLevel();
            Dungeon d2 = gst.GetDungeon();

            bool expected = false;
            bool actual = d.GetHashCode() == d2.GetHashCode() && p.get_position() == 0;

            Assert.AreEqual(expected, actual);
        }

        public void NextLevel_5()
        {
            Dungeon d = new Dungeon(5);
            Player p = new Player();
            GameState gst = new GameState(d, p);
            gst.NextLevel();
            Dungeon d2 = gst.GetDungeon();
            bool expected = false;
            bool actual = d.GetHashCode() == d2.GetHashCode() && p.get_position() == 0;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PackMoves()
        {
            Dungeon d = new Dungeon(1);
            Player p = new Player();
            GameState gst = new GameState(d, p);

            int hashBefore = gst.GetDungeon().nodes.GetHashCode();

            gst.PackMoves();

            bool expected = true;
            bool actual = hashBefore == gst.GetDungeon().nodes.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UsePotion() 
        {
            UsePotion_PotionAvailable();
            UsePotion_NoPotion();
            UsePotion_Player_FullHealth();
            UsePotion_Player_LowHealth();

        }

        public void UsePotion_PotionAvailable()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            p.add(new Magic_Scroll());
            p.add(new Health_Potion());
            int numItems = p.getItems().Count;

            gst.UsePotion();
            int numItems2 = p.getItems().Count;

            Assert.AreEqual(numItems, numItems2 + 1);
        }

        public void UsePotion_NoPotion()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            int numItems = p.getItems().Count;

            gst.UsePotion();
            int numItems2 = p.getItems().Count;

            bool expected = true;
            bool numItemsCheck = numItems == numItems2;
            bool actual = numItemsCheck && numItems == 0;

            Assert.AreEqual(expected, actual);
        }

        public void UsePotion_Player_LowHealth()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            p.set_HP(100);
            int healthBefore = p.GetHP();

            p.add(new Health_Potion());

            gst.UsePotion();

            int healthAfter = p.GetHP();

            bool expected = true;
            bool actual = true;
            actual = healthBefore < healthAfter && healthAfter <= 250;

            Assert.AreEqual(expected, actual);
        }
        public void UsePotion_Player_FullHealth()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            p.set_HP(250);
            int healthBefore = p.GetHP();

            p.add(new Health_Potion());

            gst.UsePotion();

            int healthAfter = p.GetHP();

            bool expected = true;
            bool actual = true;
            actual = healthBefore == healthAfter;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UseScroll()
        {
            UseScroll_NoScroll();
            UseScroll_ScrollAvailable();
        }

        public void UseScroll_NoScroll()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            int numItems = p.getItems().Count;

            gst.UseScroll();
            int numItems2 = p.getItems().Count;

            bool expected = true;
            bool numItemsCheck = numItems == numItems2;
            bool actual = numItemsCheck && numItems == 0;

            Assert.AreEqual(expected, actual);
        }

        public void UseScroll_ScrollAvailable()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            p.add(new Health_Potion());
            p.add(new Magic_Scroll());
            
            int numItems = p.getItems().Count;

            gst.UseScroll();
            int numItems2 = p.getItems().Count;

            Assert.AreEqual(numItems, numItems2+1);
        }

        [TestMethod]
        public void UseCrystal()
        {
            UseCrystal_NoCrystal();
            UseCrystal_CrystalAvailable();
        }

        private void UseCrystal_CrystalAvailable()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            p.add(new Health_Potion());
            p.add(new Time_Crystal());

            int numItems = p.getItems().Count;

            gst.UseCrystal();
            int numItems2 = p.getItems().Count;

            Assert.AreEqual(numItems, numItems2 + 1);
        }

        private void UseCrystal_NoCrystal()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            p.add(new Health_Potion());

            int numItems = p.getItems().Count;

            gst.UseCrystal();
            int numItems2 = p.getItems().Count;

            bool expected = true;
            bool actual = numItems == numItems2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayerDead()
        {
            PlayerDead_Full_HP();
            PlayerDead_Zero_HP();
        }
        public void PlayerDead_Zero_HP()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            p.set_HP(0);

            bool expected = true;
            bool actual = gst.PlayerDead();


            Assert.AreEqual(expected, actual);
        }

        public void PlayerDead_Full_HP()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            p.set_HP(250);

            bool expected = false;
            bool actual = gst.PlayerDead();


            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GivePackReward()
        {
            GivePackReward_Bridge();
            GivePackReward_No_Bridge();
        }

        public void GivePackReward_Bridge()
        {
            GameState gst = new GameState(5);
            Dungeon d = gst.GetDungeon();
            Player p = gst.GetPlayer();
            int scoreBefore = p.getScore();

            int interval = d.interval;

            if (d.nodes[2 * interval] == null)
                d.nodes[2 * interval] = new Node(2*interval);

            p.set_position(2 * interval);

            gst.GivePackRewardTest(10);

            int scoreAfter = p.getScore();

            bool expected = true;
            bool actual = scoreAfter > scoreBefore && scoreAfter == (scoreBefore + (2 * 10));

            Assert.AreEqual(expected, actual);
            
        }
        public void GivePackReward_No_Bridge()
        {
            GameState gst = new GameState(5);
            Dungeon d = gst.GetDungeon();
            Player p = gst.GetPlayer();
            int scoreBefore = p.getScore();

            int interval = d.interval;

            if (d.nodes[2 * interval + 1] == null)
                d.nodes[2 * interval + 1] = new Node(2 * interval + 1);

            p.set_position(2 * interval + 1);

            gst.GivePackRewardTest(10);

            int scoreAfter = p.getScore();

            bool expected = true;
            bool actual = scoreAfter > scoreBefore && scoreAfter == (scoreBefore +  10);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Fighting()
        {
            Fighting_noPack();
            Fighting_Pack();
        }

        public void Fighting_Pack()
        {
            GameState gst = new GameState(1);
            Dungeon d = gst.GetDungeon();
            Player p = gst.GetPlayer();
            int position = 2;

            if (d.nodes[position] == null)
                d.nodes[position] = new Node(position);
            if (!d.nodes[position].hasPack())
                d.nodes[position].pushPack(new Pack(2));

            p.set_position(position);

            bool expected = true;
            bool actual = gst.fighting();

            Assert.AreEqual(expected, actual);
        }
        public void Fighting_noPack()
        {
            GameState gst = new GameState(1);
            Dungeon d = gst.GetDungeon();
            Player p = gst.GetPlayer();
            int position = 2;

            if (d.nodes[position] == null)
                d.nodes[position] = new Node(position);
            while (d.nodes[position].hasPack())
                d.nodes[position].popPack();

            p.set_position(position);

            bool expected = false;
            bool actual = gst.fighting();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPosition()
        {
            SetPosition_SamePosition();
            SetPosition_DifferentPosition();
        }

        private void SetPosition_DifferentPosition()
        {
            GameState gst = new GameState(1);
            Dungeon d = gst.GetDungeon();
            Player p = gst.GetPlayer();
           
            int positionBefore = p.get_position();
            gst.SetPosition(d.nodes.Length-1);
            int positionAfter = p.get_position();

            bool expected = false;
            bool actual = positionAfter == positionBefore;

            Assert.AreEqual(expected, actual);
        }

        private void SetPosition_SamePosition()
        {
            GameState gst = new GameState(1);
            Dungeon d = gst.GetDungeon();
            Player p = gst.GetPlayer();

            int positionBefore = p.get_position();
            gst.SetPosition(0);
            int positionAfter = p.get_position();

            bool expected = true;
            bool actual = positionAfter == positionBefore;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckFinished()
        {
            CheckFinished_True();
            CheckFinished_False();
        }

        private void CheckFinished_False()
        {
            GameState gst = new GameState(1);
            Dungeon d = gst.GetDungeon();

            bool expected = false;
            bool actual = gst.CheckFinished();

            Assert.AreEqual(expected, actual);
        }

        private void CheckFinished_True()
        {
            GameState gst = new GameState(1);
            Dungeon d = gst.GetDungeon();
            Player p = gst.GetPlayer();

            gst.SetPosition(d.nodes.Length-1);

            bool expected = true;
            bool actual = gst.CheckFinished();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Fight()
        {
            Fight_PackDead();
            Fight_PackAlive_NoRetreat();
            Fight_PackAlive_Retreat();
        }

        private void Fight_PackAlive_Retreat()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            int position = 2;
            Node n = new Node(position);
            Dungeon d = gst.GetDungeon();
            d.nodes[position] = n;

            Stack<Monster> monsters = new Stack<Monster>();
            Monster monster = new Monster();
            monster.gets_hit(13);
            monsters.Push(monster);

            d.nodes[position].pushPack(new Pack(monsters));

            p.set_position(position);

            gst = new GameState(d, p);

            bool expected = true;
            bool actual = gst.Fight();

            Assert.AreEqual(expected, actual);
        }

        private void Fight_PackAlive_NoRetreat()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            int position = 2;
            Node n = new Node(position);
            Dungeon d = gst.GetDungeon();
            d.nodes[position] = n;

            d.nodes[position].pushPack(new Pack(3));

            p.set_position(position);

            gst = new GameState(d, p);

            bool expected = false;
            bool actual = gst.Fight();

            Assert.AreEqual(expected, actual);
        }

        private void Fight_PackDead()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            int position = 2;
            Node n = new Node(position);
            Dungeon d = gst.GetDungeon();
            d.nodes[position] = n;

            Stack<Monster> monsters = new Stack<Monster>();
            Monster monster = new Monster();
            monster.gets_hit(16);
            monsters.Push(monster);
            d.nodes[position].pushPack(new Pack(monsters));

            p.set_position(position);

            gst = new GameState(d, p);

            bool expected = true;
            bool actual = gst.Fight();

            Assert.AreEqual(expected, actual);
        }
    }
}
