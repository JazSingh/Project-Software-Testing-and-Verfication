using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class DungeonTests
    {
        //Test if basic properties of the map are set correctly upon initialization.
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

        //Test if the number of nodea is at least difficulty + 2
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

        //Check if a spanning tree is created between two nodes
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

        //Test if a graph is fully connected when creating a spanning tree
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

        //Check if in a dungeon with only one node, no new edges are added
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

        //Check if there are no loose end, no new edges are added
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

        //Check if a single loose end is handled correctly
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

        //Check if multiple loose end are handled correctly
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

        //Check if two paritions are connected correcly
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
            d.ConnectParition(1, 2);

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

            Assert.IsTrue(d.nodes[4].NumNeighbours == 1);
        }

        //Check if when a pack should retreat the method returns a true value
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

        //Check in case of a retreat the pack retreats to that single node
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

        //Check if the method returns a false if a pack shouldnt retreat.
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

        //Check if pack can move a from a contested node which arent engaged in battle
        [TestMethod]
        public void MovePacksFromContested()
        {
            int diff = 1;
            int dsize = 3;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[0].AddPack();
            ns[1] = new Node(1);
            ns[2] = new Node(2);

            ns[0].AddNeighbour(1);
            ns[1].AddNeighbour(0);

            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            d.MovePacks(0);
            Assert.IsFalse(d.nodes[1].hasPack());
        }

        //Check if moving packs to an empty node is possible
        [TestMethod]
        public void MovePackToEmptyNode()
        {
            int diff = 1;
            int dsize = 3;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[0].AddPack();
            ns[1] = new Node(1);
            ns[2] = new Node(2);

            ns[0].AddNeighbour(1);
            ns[1].AddNeighbour(0);

            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            d.MovePacks(2);
            Assert.IsFalse(d.nodes[0].hasPack());
        }

        //Check if it is possible to move packs to the node in which the player currently resides
        [TestMethod]
        public void MovePackToPlayerNode()
        {
            int diff = 1;
            int dsize = 3;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[1].AddPack();

            ns[0].AddNeighbour(1);
            ns[1].AddNeighbour(0);

            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            d.MovePacks(0);
            Assert.IsTrue(d.nodes[0].hasPack());
        }

        //It shouldnt be possible to move a pack to a node if it will breach its maximum capacity
        [TestMethod]
        public void MovePackMaxCap()
        {
            int diff = 1;
            int dsize = 3;
            Node[] ns = new Node[dsize];

            ns[0] = new Node(0);
            ns[0].AddPack();
            Pack p1 = new Pack(1);
            p1.hit_pack(1);
            ns[0].pushPack(p1);
            Pack p2 = new Pack(2);
            p2.hit_pack(2);
            ns[0].pushPack(p2);

            ns[1] = new Node(1);
            ns[1].AddPack();
            Pack p3 = new Pack(3);
            p3.hit_pack(3);
            ns[1].pushPack(p3);
            Pack p4 = new Pack(4);
            p4.hit_pack(4);
            ns[1].pushPack(p4);

            ns[0].AddNeighbour(1);
            ns[1].AddNeighbour(0);

            ns[2] = new Node(2);
            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            d.MovePacks(2);

            Pack p5 = new Pack(5);
            Assert.IsTrue(ns[0].popPack().GetPackHealth() == p5.GetPackHealth() - 2);
            Assert.IsTrue(ns[0].popPack().GetPackHealth() == p5.GetPackHealth() - 1);
            Assert.IsTrue(ns[1].popPack().GetPackHealth() == p5.GetPackHealth() - 4);
            Assert.IsTrue(ns[1].popPack().GetPackHealth() == p5.GetPackHealth() - 3);
        }

        //Check if packs dont disspear when moving them around
        [TestMethod]
        public void MovePack()
        {
            for (int i = 1; i <= 100; i++)
            {
                int som = 0;
                for (int j = 1; j <= i; j++)
                    som += j;

                Dungeon d = new Dungeon(i);
                d.SpawnMonsters();
                d.MovePacks(0);
                Assert.IsTrue(som + 1 >= d.SumMonsterHealth() / 45 
                    &&        som - 1 <= d.SumMonsterHealth() / 45);
            }
        }

        //Check if packs still have the same health when moves
        [TestMethod]
        public void MovePackDamaged()
        {
            int diff = 1;
            int dsize = 3;
            Node[] ns = new Node[dsize];

            ns[0] = new Node(0);
            Pack p1 = new Pack(1);
            p1.hit_pack(1);
            ns[0].pushPack(p1);
            Pack p2 = new Pack(2);
            p2.hit_pack(2);
            ns[0].pushPack(p2);

            ns[1] = new Node(1);
            Pack p3 = new Pack(3);
            p3.hit_pack(3);
            ns[1].pushPack(p3);
            Pack p4 = new Pack(4);
            p4.hit_pack(4);
            ns[1].pushPack(p4);

            ns[0].AddNeighbour(1);
            ns[1].AddNeighbour(0);

            ns[2] = new Node(2);
            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            d.MovePacks(2);

            Pack p5 = new Pack(5);
            Assert.IsTrue(ns[0].popPack().GetPackHealth() != p5.GetPackHealth() - 2
            || ns[0].popPack().GetPackHealth() != p5.GetPackHealth() - 1
            || ns[1].popPack().GetPackHealth() != p5.GetPackHealth() - 4
            || ns[1].popPack().GetPackHealth() != p5.GetPackHealth() - 3);
        }

        //Check if no edges are added in a graph with a single node
        [TestMethod]
        public void AddEdgesSingleNode()
        {
            int diff = 3;
            int dsize = 7;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            Dungeon d = new Dungeon(ns, diff, dsize, 2);
            d.AddRandomEdges(0);
            Assert.AreEqual(0, ns[0].NumNeighbours);
        }

        //Check if edges can be added
        [TestMethod]
        public void AddEdgesMultipleNodesNoNeigs()
        {
            int diff = 3;
            int dsize = 12;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);
            Dungeon d = new Dungeon(ns, diff, dsize, 4);
            d.AddRandomEdges(0);
            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(ns[i].NumNeighbours >= 0);
            }
        }

        //Test if the length of the shortest path is actually minimal
        [TestMethod]
        public void SPlength()
        {
            int diff = 1;
            int dsize = 4;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);

            ns[0].AddNeighbour(1); ns[1].AddNeighbour(0);
            ns[1].AddNeighbour(2); ns[2].AddNeighbour(1);
            ns[2].AddNeighbour(3); ns[3].AddNeighbour(2);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            Stack<Node> vs1 = d.ShortestPath(ns[0], ns[1]);
            Stack<Node> vs2 = d.ShortestPath(ns[0], ns[2]);
            Stack<Node> vs3 = d.ShortestPath(ns[0], ns[3]);

            Assert.AreEqual(2, vs1.Count);
            Assert.AreEqual(3, vs2.Count);
            Assert.AreEqual(4, vs3.Count);
        }

        //If the graph contains a cycle, check if the given path is still minimal
        [TestMethod]
        public void SPCycle()
        {
            int diff = 1;
            int dsize = 5;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);
            ns[4] = new Node(4);

            ns[0].AddNeighbour(1); ns[1].AddNeighbour(0);
            ns[1].AddNeighbour(2); ns[2].AddNeighbour(1);
            ns[2].AddNeighbour(3); ns[3].AddNeighbour(2);

            ns[4].AddNeighbour(1); ns[1].AddNeighbour(4);
            ns[4].AddNeighbour(2); ns[2].AddNeighbour(4);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            Stack<Node> vs1 = d.ShortestPath(ns[0], ns[3]);
            Assert.AreEqual(4, vs1.Count);
        }

        //Check for the shortest path
        [TestMethod]
        public void SP()
        {
            int diff = 1;
            int dsize = 5;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);
            ns[4] = new Node(4);

            ns[0].AddNeighbour(1); ns[1].AddNeighbour(0);
            ns[1].AddNeighbour(2); ns[2].AddNeighbour(1);
            ns[2].AddNeighbour(3); ns[3].AddNeighbour(2);

            ns[4].AddNeighbour(0); ns[0].AddNeighbour(4);
            ns[4].AddNeighbour(3); ns[3].AddNeighbour(4);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            Stack<Node> vs1 = d.ShortestPath(ns[0], ns[3]);
            Assert.AreEqual(3, vs1.Count);
        }

        //Chick if upon creation, all nodes are reachable from the exit
        [TestMethod]
        public void AllReachable()
        {
            for (int i = 1; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i);
                int rnodes = CountNonNullNodes(d);
                for (int j = 0; j < d.dungeonSize; j++)
                    if (d.nodes[j] != null)
                    {
                        bool[] rs = new bool[d.dungeonSize];
                        d.ReachableNodes(d.nodes[j], ref rs);
                        Assert.AreEqual(rnodes, CountTrue(rs));
                    }
            }
        }

        //If we remove a bridge, all nodes preceiding it, shouldnt be reachable
        [TestMethod]
        public void DestroyImportant()
        {
            for (int i = 4; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i);
                int result = d.Destroy(d.nodes[d.interval * (i / 2)]);
                for (int j = 0; j <= d.interval * (i / 2); j++)
                { 
                    Assert.IsNull(d.nodes[j]);
                }
                Assert.AreNotSame(-1, result);
            }
        }

        //Check all other nodes are reachable if we destroy a node which only has one incoming edge.
        [TestMethod]
        public void DestroyNonImportant()
        {
            int diff = 1;
            int dsize = 4;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);

            ns[0].AddNeighbour(1); ns[1].AddNeighbour(0);
            ns[0].AddNeighbour(2); ns[2].AddNeighbour(0);
            ns[2].AddNeighbour(3); ns[3].AddNeighbour(2);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            d.Destroy(ns[1]);
            Assert.AreEqual(3, CountNonNullNodes(d));
        }

        //Test if dropping different kind of item works
        [TestMethod]
        public void DropHealth()
        {
            int s = 0;
            Dungeon d = new Dungeon(10);
            d.DropItem(ItemType.HealthPotion);
            for (int i = 0; i < d.dungeonSize; i++)
            {
                if (d.nodes[i] != null && d.nodes[i].get_Items().Count > 0) s++;
            }
            Assert.AreEqual(1, s);
        }

        [TestMethod]
        public void DropCrystal()
        {
            int s = 0;
            Dungeon d = new Dungeon(10);
            d.DropItem(ItemType.TimeCrystal);
            for (int i = 0; i < d.dungeonSize; i++)
            {
                if (d.nodes[i] != null && d.nodes[i].get_Items().Count > 0) s++;
            }
            Assert.AreEqual(1, s);
        }

        [TestMethod]
        public void DropScroll()
        {
            int s = 0;
            Dungeon d = new Dungeon(10);
            d.DropItem(ItemType.MagicScroll);
            for (int i = 0; i < d.dungeonSize; i++)
            {
                if (d.nodes[i] != null && d.nodes[i].get_Items().Count > 0) s++;
            }
            Assert.AreEqual(1, s);
        }

        //Check if the right amount of monsters are spawned
        [TestMethod]
        public void SpawnMonsters()
        {
            for (int i = 1; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i);
                d.SpawnMonsters();
                int exp = d.SumMonsterHealth();
                int act = 0;
                for (int j = 0; j < d.dungeonSize; j++)
                {
                    if (d.nodes[j] != null) act += d.nodes[j].SumMonsterHealth();
                }
                Assert.AreEqual(exp, act);
            }

        }

        //Check if the sum of health of the monsters equals zero, if no monsters are on the map
        [TestMethod]
        public void SumMonstHealthNone()
        {
            for (int i = 1; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i);
                int act = 0;
                for (int j = 0; j < d.dungeonSize; j++)
                {
                    if (d.nodes[j] != null) act += d.nodes[j].SumMonsterHealth();
                }
                Assert.AreEqual(0, act);
            }
        }

        //Check if the sum of the health is correct when some monsters are damaged
        [TestMethod]
        public void SumMonstHealthDamaged()
        {
            Dungeon d = new Dungeon(10);
            d.nodes[0].AddPack();
            Pack p = d.nodes[0].popPack();
            p.hit_pack(10);
            d.nodes[0].pushPack(p);
            int exp = d.SumMonsterHealth();
            int act = 0;
            for (int j = 0; j < d.dungeonSize; j++)
            {
                if (d.nodes[j] != null) act += d.nodes[j].SumMonsterHealth();
            }
            Assert.AreEqual(exp, act);
        }

        //Check if the sum of the health of the monsters is correct if none are damaged
        [TestMethod]
        public void SumMonstHealthFull()
        {
            for (int i = 1; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i);
                d.SpawnMonsters();
                int exp = d.SumMonsterHealth();
                int act = 0;
                for (int j = 0; j < d.dungeonSize; j++)
                {
                    if (d.nodes[j] != null) act += d.nodes[j].SumMonsterHealth();
                }
                Assert.AreEqual(exp, act);
            }
        }

        //Same two tests for health potions as above
        [TestMethod]
        public void SumHPotsNone()
        {
            for (int i = 1; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i);
                int act = 0;
                for (int j = 0; j < d.dungeonSize; j++)
                {
                    if (d.nodes[j] != null) act += d.nodes[j].SumHealPots();
                }
                Assert.AreEqual(0, act);
            }
        }

        [TestMethod]
        public void SumHPotsSome()
        {
            for (int i = 1; i <= 100; i++)
            {
                Dungeon d = new Dungeon(i);
                Health_Potion hp = new Health_Potion();
                int exp = hp.health;
                d.DropItem(ItemType.HealthPotion);
                int act = 0;
                for (int j = 0; j < d.dungeonSize; j++)
                {
                    if (d.nodes[j] != null) act += d.nodes[j].SumHealPots();
                }
                Assert.AreEqual(exp, act);
            }
        }

        //Check if no nodes are reachable if no edges are present.
        [TestMethod]
        public void ReachableNodesNoNeighbours()
        {
            int diff = 1;
            int dsize = 4;
            Node[] ns = new Node[dsize];
            ns[0] = new Node(0);
            ns[1] = new Node(1);
            ns[2] = new Node(2);
            ns[3] = new Node(3);

            ns[0].AddNeighbour(1); ns[1].AddNeighbour(0);
            ns[0].AddNeighbour(2); ns[2].AddNeighbour(0);

            Dungeon d = new Dungeon(ns, diff, dsize, 1);
            bool[] b = new bool[4];

            d.ReachableNodes(ns[3], ref b);
            Assert.IsTrue(b[3]);
            Assert.IsFalse(b[2]);
            Assert.IsFalse(b[1]);
            Assert.IsFalse(b[0]);
        }

        //Help Methods
        private int CountNonNullNodes(Dungeon d)
        {
            int t = 0;
            for (int i = 0; i < d.dungeonSize; i++)
                if (d.nodes[i] != null) t++;
            return t;
        }

        private int CountTrue(bool[] b)
        {
            int k = 0;
            foreach (bool tf in b)
                if (tf) k++;
            return k;
        }
    }
}
