using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

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
            bool actual = d.GetHashCode() == d2.GetHashCode();

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
            bool actual = d.GetHashCode() == d2.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]

        public void PackMoves()
        {
            Dungeon d = new Dungeon(1);
            Player p = new Player();
            GameState gst = new GameState(d, p);
            int hash = gst.GetHashCode();
            gst.PackMoves();

            bool expected = true;
            bool actual = hash == gst.GetHashCode();

            Assert.AreEqual(expected, actual);
        }
    
    }
}
