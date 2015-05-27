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
        // create a gamestate with a dungeon and a player, and checks wether the provided dungeon is the same as the dungeon
        // GetDungeon() returns by comparing hashes
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

        // create a gamestate with a dungeon and a player, and checks wether the provided player is the same as 
        // the player that gets returned by GePlayer() by comparing hashes
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

        // checks wether the NextLevel method returns a new level
        [TestMethod]
        public void NextLevel()
        {
            NextLevel_5();
            NextLevel_Not5();
        }

        // Create a new GameState with a Player and a level 4 dungeon
        // check if the dungeons are not identical
        // Check wether the new dungeon is a level higher and 
        // check if the player starts at position 0
        private void NextLevel_Not5()
        {
            int difficulty = 4;
            Dungeon d = new Dungeon(difficulty);
            Player p = new Player();
            GameState gst = new GameState(d, p);
            gst.NextLevel();
            Dungeon d2 = gst.GetDungeon();
            Player p2 = gst.GetPlayer();

            bool expected = true;
            bool actual = d.GetHashCode() != d2.GetHashCode() && p2.get_position() == 0 && d2.difficulty == difficulty+1;

            Assert.AreEqual(expected, actual);
        }

        // same as above but with a level 5 dungeon
        // check wether the level of the new dungeon is also 5
        public void NextLevel_5()
        {
            Dungeon d = new Dungeon(5);
            Player p = new Player();
            GameState gst = new GameState(d, p);
            gst.NextLevel();
            Dungeon d2 = gst.GetDungeon();
            Player p2 = gst.GetPlayer();

            bool expected = true;
            bool actual = d.GetHashCode() != d2.GetHashCode() && p2.get_position() == 0 && d2.difficulty == 5;

            Assert.AreEqual(expected, actual);
        }

        // Check wether the packs in a dungeon move to different nodes 
        // when PackMoves() is called
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

        //Check wether a potion has been used when a potion is available by comparing the player backpack before and after usage
        [TestMethod]
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

        // same as above, but no health potion is available
        [TestMethod]
        public void UsePotion_NoPotion()
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

            p.set_HP(10);
            gst.UsePotion();

            Assert.IsTrue(p.GetHP() == 10);
        }

        // Check wether using a health potion actually heals the player
        // check if the HP stays below 250
        [TestMethod]
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

        // checks if the current HP can surpass the max HP after using a potion
        [TestMethod]
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

        // check wether a scroll is used when none are available
        [TestMethod]
        public void UseScroll_NoScroll()
        {
            Player p = new Player();
            Dungeon d = new Dungeon(5);
            GameState gst = new GameState(d, p, true);


            gst.UseScroll();
            int numItems2 = p.getItems().Count;

            Assert.AreEqual(0, numItems2);
        }

        // check wether a scroll is used when there is a scroll available
        [TestMethod]
        public void UseScroll_ScrollAvailable()
        {
            Oracle.DETERM = true;
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();
            p.add(new Health_Potion());
            p.add(new Magic_Scroll());
            p.add(new Magic_Scroll());
            
            int numItems = p.getItems().Count;

            gst.UseScroll();
            Oracle.DETERMF = true;
            gst.UseScroll();
            gst.UseScroll();

            int numItems2 = p.getItems().Count;
            Assert.AreEqual(1, numItems2);
        }

        [TestMethod]
        public void UseCrystal()
        {
            UseCrystal_NoCrystal();
            UseCrystal_CrystalAvailable();
        }

        // checks wether a crystal is used while there isn't any available
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

        // checks wether a crystal is used
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

        // check if PlayerDead returns true when the player has no HP left
        public void PlayerDead_Zero_HP()
        {
            GameState gst = new GameState(1);
            Player p = gst.GetPlayer();

            p.set_HP(0);

            bool expected = true;
            bool actual = gst.PlayerDead();


            Assert.AreEqual(expected, actual);
        }

        // check if PlayerDead returns false if the player still has some HP left
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

        // check wether more points are rewarded when the battle took place on a bridge
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

        // check wether the normal amount of points has been rewarded if the battle did not took place on a bridge
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

        // check wether Fighting() returns true when there still is a pack left to fight
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

        // check wether Fighting() returns false when no pack is present
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

        // check if the players position changes when SetPosition is called on a different node
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

        // check if the players position remains the same when SetPosition is called on the same node
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

        // check wether CheckFinished returns false if the player has not yet reached the end node
        private void CheckFinished_False()
        {
            GameState gst = new GameState(1);
            Dungeon d = gst.GetDungeon();

            bool expected = false;
            bool actual = gst.CheckFinished();

            Assert.AreEqual(expected, actual);
        }

        // check wether CheckFinished returns true if the player has reached the end node
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

        [TestMethod]
        public void FightInNode()
        {
            // checks if a player is in a node which has multiple packs,
            // the player only attacks one pack.
            GameState gst = new GameState(3);
            Player p = gst.GetPlayer();
            int position = 2;
            p.set_position(position);
            Node n = new Node(position);
            n.AddPack();
            n.AddPack();
            Dungeon d = gst.GetDungeon();
            d.nodes[position] = n;
            bool actual = gst.Fight();
            Assert.AreEqual(false, actual);


            Stack<Pack> s = d.nodes[position].getPacks();
            for (int t =0;t<2;t++)
            {
                Pack pak = s.Pop();
                if (t ==0)
                {
                    Assert.AreEqual(7, pak.getCurrent().GetHP());
                    Assert.AreEqual(15, pak.getMonsters().Pop().GetHP());
                    Assert.AreEqual(15, pak.getMonsters().Pop().GetHP());
                }
                else if (t==1)
                {
                    Assert.AreEqual(15, pak.getMonsters().Pop().GetHP());
                    Assert.AreEqual(15, pak.getMonsters().Pop().GetHP());
                    Assert.AreEqual(15, pak.getMonsters().Pop().GetHP());
                }
            }

        }

        // check if Fight returns true if there is a pack remaining but it retreats
        private void Fight_PackAlive_Retreat()
        {
            GameState gst = new GameState(3);
            Player p = gst.GetPlayer();

            int position = 2;
            Node n = new Node(position);
            Dungeon d = gst.GetDungeon();
            d.nodes[position] = n;
            d.nodes[3] = new Node(3);
            d.nodes[3].AddNeighbour(2); d.nodes[2].AddNeighbour(3);

            Stack<Monster> monsters = new Stack<Monster>();
            Monster monster = new Monster();
            monsters.Push(monster);

            Pack pak = new Pack(monsters);
            pak.hit_pack(13);
            d.nodes[position].pushPack(pak);

            p.set_position(position);

            gst = new GameState(d, p, true);

            bool expected = true;
            bool actual = gst.Fight();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RetFighting()
        {
            int diff = 3;
            int dsize = 7;
            Node[] ns = new Node[dsize];
            ns[1] = new Node(1);
            ns[1].AddPack();
            ns[1].AddPack();
            ns[2] = new Node(2);

            ns[3] = new Node(3);

            ns[1].AddNeighbour(2);
            ns[2].AddNeighbour(1);
            ns[1].AddNeighbour(3);
            ns[3].AddNeighbour(1);

            Pack p = ns[1].popPack();
            p.hit_pack(45);
            p.hit_pack(45);
            p.hit_pack(3);
            ns[1].pushPack(p);
            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            Player pl = new Player();
            pl.set_position(1);

            GameState gst = new GameState(d, pl, true);
            Assert.IsTrue(gst.Fight());
        }

        // check if Fight returns false if the pack shouldn't retreat
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

        // check if Fight returns true if all packs have been slain
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

        // check if the player does not pick up any items when none are available
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

        // check if the player picks up all items from the node if there are items available
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

        // php is the players current hp
        // b wheter health potions should be spawned in all nodes
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

        // check wether the pack retreats when it should
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

        // check if a pack doesn't retreat if it shouldn't 
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

        //check wether after saving a safe file is available
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
