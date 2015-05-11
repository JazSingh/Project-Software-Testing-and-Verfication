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
            // pre: player has score 0 before killing pack
            // post: player has score 9, after killing pack
            Pack p = new Pack(10);
            int expected = 9;
            int actual = p.get_Score();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Retreat()
        {
            // tests if a pack retreats correctly, saying
            // the pack retreats when hp <= 30% of initial hp, otherwise it doesn't retreat
            Retreat_False();
            Retreat_True();
        }

        public void Retreat_False()
        {
            // new pack is hit once, but doesn't retreat
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.retreat();
            Assert.AreEqual(expected, actual);
        }

        public void Retreat_True()
        {
            // new pack is hit multiple times,
            // and retreats
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
            // tests the pack's status
            isDead_False();
            isDead_True();
            isDead_CurrentNull_False();
        }

        public void isDead_False()
        {
            // pre: new pack is being made
            // post: pack is alive
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.isDead();
            Assert.AreEqual(expected, actual);
        }

        public void isDead_CurrentNull_False()
        {
            // pre: new pack
            // post: pack is, after being hit, still alive
            Pack p = new Pack(10);
            p.hit_pack(16);
            bool expected = false;
            bool actual = p.isDead();
            Assert.AreEqual(expected, actual);
        }

        public void isDead_True()
        {
            // pre: new pack is alive
            // post: pack is, after being hit multiple times, dead
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
            // tests the numMonsters-variable of Pack
            GetNumMonsters_NewPack();
            GetNumMonsters_NoCurrent();
        }
        public void GetNumMonsters_NewPack()
        {
            // tests if a new pack returns the correct number of monsters
            Pack p = new Pack(10);
            int expected = 3;
            int actual = p.GetNumMonsters();
            Assert.AreEqual(expected, actual);
        }

        public void GetNumMonsters_NoCurrent()
        {
            // tests if a pack still returns the correct number of monsters,
            // after being attacked
            Pack p = new Pack(10);
            p.hit_pack(16);
            int expected = 2;
            int actual = p.GetNumMonsters();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddMonster()
        {
            // pre: pack has x HP and y monsters
            // post: pack has, after a monster M gets added, has an HP of x+M.hp, 
            // and y+1 monsters
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
            // tests the getInitalHP()-method of Pack
            Pack p = new Pack(10);
            int expected = 3 * 15;
            int actual = p.getInitialHP();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPackHealth()
        {
            // tests if a pack returns it's health correctly
            GetPackHealth_FreshPack();
            GetPackHealth_CurrentNull();

        }
        public void GetPackHealth_FreshPack()
        {
            // tests if a new pack returns it's health correctly
            Pack p = new Pack(10);
            int expected = 3 * 15;
            int actual = p.GetPackHealth();
            Assert.AreEqual(expected, actual);
        }

        public void GetPackHealth_CurrentNull()
        {
            // tests if an attacked pack, still alive, returns it's health correctly
            Pack p = new Pack(10);
            p.hit_pack(16);
            int expected = 2 * 15;
            int actual = p.GetPackHealth();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Gets_Hit()
        {
            // tests if a pack-object's process of a certain hit works correctly
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
            // pre: new pack, alive
            // post: pack is dead, after being hit multiple times
            Pack p = new Pack(10);
            p.hit_pack(16);
            p.hit_pack(16);
            p.hit_pack(16);
            p.hit_pack(16);
            Assert.AreEqual(true, p.isDead());
        }

        public void Gets_hit_False()
        {
            // pre: new pack, alive
            // post: pack gets hit, still alive
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.hit_pack(8);
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_CompleteFight()
        {
            // pre: new pack, alive
            // post: pack is dead, after each monster gets killed
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
            // pre: new pack, alive
            // post: pack is still alive, after being hit vy a time crystal (hits 14)
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(14); // attack with 14 damage
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_Crystal_True()
        {
            // pre: new pack, alive
            // post: pack is killed, after being hit by a time crystal (hits 15)
            Pack p = new Pack(10);
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(15); // attack with 15 damage
            Assert.AreNotEqual(expected, actual);
        }

        public void Gets_hit_Crystal_CurrentNull_15dmg()
        {
            // pre: pack (2/3 monsters alive)
            // post: pack is killed, after being hit by a time crystal (hits 15)
            Pack p = new Pack(10);
            p.hit_pack(16);
            bool expected = true;
            bool actual = p.hit_pack_Time_Crystal_variant(15);
            Assert.AreEqual(expected, actual);
        }

        public void Gets_hit_Crystal_CurrentNull_14dmg()
        {
            // pre: pack (2/3 monsters alive)
            // post: pack is still alive, after being hit by time crystal (hits 14)
            Pack p = new Pack(10);
            p.hit_pack(16);
            bool expected = false;
            bool actual = p.hit_pack_Time_Crystal_variant(14);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void hit_player()
        {
            // pre: players' hp = X
            // post: after being attacked by a pack once, players' hp = X - 3*3
            Pack p = new Pack(10);
            int expected = 3 * 3;
            int actual = p.hit_player();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetItem()
        {
            // tests if get'Item'-methods work correctly
            GetItem_HP();
            GetItem_MS();
            GetItem_TC();
        }

        public void GetItem_HP() 
        {
            // pre: pack has a potion as item
            // post: pack returned a healthPotion
            Pack p = new Pack(1);
            string expected = "HealthPotion";
            string actual = p.GetItem().ToString();
            Assert.AreEqual(expected, actual);
        }
        public void GetItem_TC() 
        {
            // pre: pack has a Time Crystal as item
            // post: pack returned a Time Crystal
            Pack p = new Pack(4);
            string expected = "TimeCrystal";
            string actual = p.GetItem().ToString();
            Assert.AreEqual(expected, actual);
        }
        public void GetItem_MS() 
        {
            // pre: pack has a Magic Scroll as item
            // post: pack returned a Magic Scroll
            Pack p = new Pack(7);
            string expected = "MagicScroll";
            string actual = p.GetItem().ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
