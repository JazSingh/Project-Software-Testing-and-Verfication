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
            for (int i = 1; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i, false);
                d.nodes[0] = new Node(0);
                d.nodes[d.dungeonSize - 1] = new Node(d.dungeonSize - 1);

                d.CreateNodes();
                Assert.IsTrue(CountNonNullNodes(d) >= i + 2);
            }
        }

        [TestMethod]
        public void CreateSTreeTwoNodes()
        {
            int diff = 5;
            int dsize = 5 * 5 + 5 + 2;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[dsize - 1] = new Node(dsize - 1);
            Dungeon d = new Dungeon(ns, 5, dsize, diff);
            d.CreateSpanningTree(0);
            Assert.IsTrue(d.nodes[0].IsNeighbour(1) && d.nodes[1].IsNeighbour(0));
        }

        [TestMethod]
        public void CreateSTree()
        {
            int diff = 5;
            int dsize = 5 * 5 + 5 + 2;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);
            ns[4] = new Node(4);
            ns[dsize - 1] = new Node(dsize - 1);
            Dungeon d = new Dungeon(ns, 5, dsize, diff);
            d.CreateSpanningTree(0);
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(d.nodes[i].NumNeighbours > 0);
            }

            Assert.IsTrue(d.nodes[dsize - 1].NumNeighbours == 0);
        }

        [TestMethod]
        public void CreateSTreeSingle()
        {
            int diff = 5;
            int dsize = 5 * 5 + 5 + 2;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);

            Dungeon d = new Dungeon(ns, 5, dsize, diff);
            d.CreateSpanningTree(0);

            Assert.IsTrue(d.nodes[0].NumNeighbours == 0);
        }

        [TestMethod]
        public void FixLooseEndsNoLooseEnd()
        {
            int diff = 1;
            int dsize = 1 * 1 + 1 + 2;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);

            ns[0].AddNeighbour(1); ns[1].AddNeighbour(0);
            ns[1].AddNeighbour(2); ns[2].AddNeighbour(1);
            ns[2].AddNeighbour(3); ns[3].AddNeighbour(2);

            Dungeon d = new Dungeon(ns, 2, dsize, diff);
            d.FixLooseEnds();

            Assert.IsTrue(ns[0].NumNeighbours == 1);
            Assert.IsTrue(ns[1].NumNeighbours == 2);
            Assert.IsTrue(ns[2].NumNeighbours == 2);
            Assert.IsTrue(ns[3].NumNeighbours == 1);
        }

        [TestMethod]
        public void FixLooseEndsSingle()
        {
            int diff = 1;
            int dsize = 7;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[3] = new Node(3);
            ns[4] = new Node(4);
            ns[5] = new Node(5);
            ns[6] = new Node(6);

            ns[0].AddNeighbour(3); ns[3].AddNeighbour(0);
            ns[3].AddNeighbour(4); ns[4].AddNeighbour(3);
            ns[4].AddNeighbour(6); ns[6].AddNeighbour(4);
            ns[6].AddNeighbour(5); ns[5].AddNeighbour(6);

            Dungeon d = new Dungeon(ns, diff, dsize, 3);
            Assert.IsTrue(ns[5].NumNeighbours == 1);

            d.FixLooseEnds();

            Assert.IsTrue(ns[5].NumNeighbours == 2);
        }

        [TestMethod]
        public void FixLooseEndsMultiple()
        {
            int diff = 1;
            int dsize = 9;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[7] = new Node(7);
            ns[4] = new Node(4);
            ns[5] = new Node(5);
            ns[6] = new Node(6);
            ns[8] = new Node(8);


            ns[0].AddNeighbour(4); ns[4].AddNeighbour(0);
            ns[4].AddNeighbour(5); ns[5].AddNeighbour(4);
            ns[5].AddNeighbour(8); ns[8].AddNeighbour(5);
            ns[6].AddNeighbour(8); ns[8].AddNeighbour(6);
            ns[7].AddNeighbour(8); ns[8].AddNeighbour(7);

            Dungeon d = new Dungeon(ns, diff, dsize, 4);
            Assert.IsTrue(ns[6].NumNeighbours == 1);
            Assert.IsTrue(ns[7].NumNeighbours == 1);


            d.FixLooseEnds();

            Assert.IsTrue(ns[6].NumNeighbours == 2);
            Assert.IsTrue(ns[7].NumNeighbours == 2);

        }

        [TestMethod]
        public void ConnectPartitionTwoBridgesOnly()
        {
            int diff = 3;
            int dsize = 4;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            d.ConnectParition(1,2);

            Assert.IsTrue(d.nodes[1].IsNeighbour(2));
            Assert.IsTrue(d.nodes[2].IsNeighbour(1));
        }

        [TestMethod]
        public void ConnectPartition()
        {
            int diff = 3;
            int dsize = 7;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[6] = new Node(6);
            ns[2] = new Node(2);
            ns[3] = new Node(3);
            ns[4] = new Node(4);

            ns[2].AddNeighbour(3); ns[3].AddNeighbour(2);

            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            d.ConnectParition(1, 2);

            Assert.IsTrue(d.nodes[2].NumNeighbours == 1);
        }

        [TestMethod]
        public void CheckRetreatYes()
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
            p.hit_pack(44);
            ns[1].pushPack(p);
            Dungeon d = new Dungeon(ns, diff, dsize, 2);

            Assert.IsTrue(d.CheckRetreat(1));
        }

        [TestMethod]
        public void CheckRetreatYesOneChoice()
        {
            int diff = 3;
            int dsize = 7;
            Node[] ns = new Node[dsize];
            ns[1] = new Node(1);
            ns[1].AddPack();
            ns[1].AddPack();
            ns[2] = new Node(2);

            ns[1].AddNeighbour(2);
            ns[2].AddNeighbour(1);

            Pack p = ns[1].popPack();
            p.hit_pack(45);
            p.hit_pack(45);
            p.hit_pack(44);
            ns[1].pushPack(p);
            Dungeon d = new Dungeon(ns, diff, dsize, 2);

            Assert.IsTrue(d.CheckRetreat(1));
        }

        [TestMethod]
        public void CheckRetreatNo()
        {
            int diff = 3;
            int dsize = 7;
            Node[] ns = new Node[dsize];
            ns[1] = new Node(1);
            ns[1].AddPack();
            ns[1].AddPack();
            Dungeon d = new Dungeon(ns, diff, dsize, 2);

            Assert.IsFalse(d.CheckRetreat(1));
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
