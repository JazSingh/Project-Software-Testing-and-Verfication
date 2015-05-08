using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System.Collections.Generic;
using System.IO;

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
            Oracle.DETERM = true;
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
            
            d.nodes[position] = new Node(position);
            
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
            
            d.nodes[position] = new Node(position);
            
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

        [TestMethod]
        public void CheckItemsFound()
        {
            CheckItemsFound_ZeroItems();
            CheckItemsFound_Items();
        }

        private void CheckItemsFound_ZeroItems()
        {
            Player p = new Player();
            int diff = 1;
            int dsize = 3;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[2] = new Node(2);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            GameState gst = new GameState(d,p, true);
            int items1 = gst.GetDungeon().nodes[0].get_Items().Count;

            gst.SetPosition(0);
            gst.CheckItemsFound();

            int items = gst.GetDungeon().nodes[0].get_Items().Count;

            Assert.AreEqual(0, items1);
            Assert.AreEqual(items1, items);
        }

        private void CheckItemsFound_Items()
        {
            Player p = new Player();
            int diff = 1;
            int dsize = 3;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[2] = new Node(2);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            GameState gst = new GameState(d, p, true);

            gst.SetPosition(0);
            ns[0].Add_Item(new Health_Potion());
            ns[0].Add_Item(new Magic_Scroll());
            ns[0].Add_Item(new Time_Crystal());

            int items1 = gst.GetDungeon().nodes[0].get_Items().Count;
            gst.CheckItemsFound();
            int items = gst.GetDungeon().nodes[0].get_Items().Count;

            Assert.AreEqual(3, items1);
            Assert.AreEqual(0, items);
        }

        [TestMethod]
        public void SumPlayerPotsHP()
        {
            SumPlayerPotsHP_Test(0, false);
            SumPlayerPotsHP_Test(250, false);
            SumPlayerPotsHP_Test(0, true);
            SumPlayerPotsHP_Test(250, true);
        }

        public void SumPlayerPotsHP_Test(int php, bool b)
        {
            Dungeon d = new Dungeon(1);
            for (int i = 0; i < d.nodes.Length; i++)
            { 
                d.nodes[i] = new Node(i);
                if(b)
                    d.nodes[i].Add_Item(new Health_Potion());
            }
            Player p = new Player(250, php, 8,0,null, new List<Item>());

            GameState gst = new GameState(d,p);

            int hp = gst.SumPlayerPotsHPTest();
            int playerHP = p.GetHP();
            int dungeonHP = d.SumHealPots();
            bool expected = true;
            bool actual = hp == playerHP + dungeonHP && hp >= playerHP && hp >= dungeonHP;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckRetreat()
        {
            CheckRetreat_False();
            CheckRetreat_True();
        }

        public void CheckRetreat_True()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            Dungeon d = gst.GetDungeon();

            for (int i = 0; i < d.nodes.Length - 1; i++)
            {
                d.nodes[i] = new Node(i);
            }

            int position = 2;
            Node n = new Node(position);
            n.AddPack();
            Pack pack = n.popPack();
            pack.hit_pack(16);
            pack.hit_pack(16);
            pack.hit_pack(10);  //Alleen laatste leeft nog, hp < init/3
            n.pushPack(pack);

            d.nodes[position] = n;

            p.set_position(position);

            gst = new GameState(d, p);

            bool expected = true;
            bool actual = gst.CheckRetreat();

            Assert.AreEqual(expected, actual);
        }

        public void CheckRetreat_False()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            Dungeon d = gst.GetDungeon();

            for (int i = 0; i < d.nodes.Length - 1; i++)
            {
                d.nodes[i] = new Node(i);
            }


            int position = 2;
            Node n = new Node(position);
            n.AddPack();    //Allen leven nog, hp == init

            d.nodes[position] = n;

            p.set_position(position);

            gst = new GameState(d, p);

            bool expected = false;
            bool actual = gst.CheckRetreat();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Save()
        {
            Player p = new Player();
            Dungeon d = new Dungeon(1);

            GameState gst = new GameState(d, p);
            gst.Save("test");

            bool expected = true;
            bool actual = File.Exists("test");

            Assert.AreEqual(expected, actual);
        }

    }
}
