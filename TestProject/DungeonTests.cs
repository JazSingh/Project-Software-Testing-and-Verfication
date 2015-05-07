using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class DungeonTests
    {
        [TestMethod]
        public void BasicProperties()
        {
            Dungeon d = new Dungeon(5);
            Assert.IsNotNull(d.nodes);
            Assert.IsTrue(d.difficulty > 0);
            Assert.IsTrue(d.interval > 0);
            Assert.IsTrue(d.dungeonSize > 0);
            Assert.IsTrue(d.dungeonSize > d.interval);
            Assert.IsTrue(CountNonNullNodes(d) >= d.difficulty + 2);
        }

        [TestMethod]
        public void CreateNodes()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CreateSTreeSingleNode()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CreateSTree()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void FixLooseEndsNoLooseEnd()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void FixLooseEndsSingle()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void FixLooseEndsMultiple()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void ConnectPartitionTwoBridgesOnly()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void ConnectPartition()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void CheckRetreatYes()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void CheckRetreatNo()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void MovePacksToContested()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void MovePackToEmptyNode()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void MovePackMaxCap()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void MovePack()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void MovePackDamaged()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void AddEdgesSingleNode()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void AddEdgesMultipleNodes()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void AddEdgesSelfFull()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void AddEdgesOtherFull()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SPIncremental()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SPCycle()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SP()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void AllReachable()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void NonReachable()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SomeReachable()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void DestroyBridge()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void DestroySingleNonImportant()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void DestroyWithMultiplePaths()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void DropHealth()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void DropCrystal()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void DropScroll()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SpawnMonsters()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void GetNullNode()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void GetNonNullNode()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SumMonstHealthNone()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SumMonstHealthDamaged()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SumMonstHealthFull()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SumHPotsNone()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void SumHPotsSome()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void ToStringAny()
        {
            Assert.Fail();
            
        }

        //Help Methods
        private int CountNonNullNodes(Dungeon d)
        {
            int t = 0;
            for (int i = 0; i < d.dungeonSize; i++)
                if (d.nodes[i] != null) t++;
            return t;
        }




    }
}
