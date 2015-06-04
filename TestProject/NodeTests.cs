using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class NodeTests
    {
        //Check the number of neighbours if a node has none
        [TestMethod]
        public void NoNeighboursLength()
        {
            Node n = new Node(0);
            Assert.AreEqual(n.NumNeighbours, 0);
        }

        //Check number of neighbours if a node has some
        [TestMethod]
        public void SomeNeighboursLength()
        {
            Node n = new Node(1, new int[] { 2, 3, 4 });
            Assert.AreEqual(3, n.NumNeighbours);
        }

        //Check if node always has a degree of 4
        [TestMethod]
        public void AdjLength()
        {
            for (int i = 0; i < 20; i++)
            {
                Node n = new Node(i);
                Assert.AreEqual(4, n.get_Adj().Length);
            }
        }

        //Check for capacity
        [TestMethod]
        public void CapacityNormal()
        {
            Node n = new Node(3);
            Assert.AreEqual(9, n.GetCapacity());
        }

        //Check for capacity of bridge nodes
        [TestMethod]
        public void CapacityBridge()
        {
            for (int i = 1; i <= 100; i++)
            {
                Node n = new Node(3);
                n.SetCapacity(i);
                Assert.AreEqual(i * 9, n.GetCapacity());
            }
        }

        //Check for capacity of starting node
        [TestMethod]
        public void BridgeStartNode()
        {
            Node n = new Node(0);
            for (int i = 4; i < 7; i++)
                Assert.IsFalse(n.IsBridge(i));
        }

        //Check if node has correct indentiefier
        [TestMethod]
        public void GetIdentifier()
        {
            int k = 12;
            Node n = new Node(12);
            Assert.AreEqual(k, n.ID);
        }

        //Check if uppon initialization a node contains no items
        [TestMethod]
        public void NoItems()
        {
            Node n = new Node(1);
            Assert.AreEqual(0, n.get_Items().Count);
        }

        //Check if adding items works
        [TestMethod]
        public void AddItems()
        {
            Node n = new Node(0);
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0) n.Add_Item(new Health_Potion());
                if (i % 3 == 1) n.Add_Item(new Time_Crystal());
                if (i % 3 == 2) n.Add_Item(new Magic_Scroll());

                Assert.AreEqual(i, n.get_Items().Count);
            }
        }

        //Removing from an empty node should still result in the node containing 0 items
        [TestMethod]
        public void RemoveItemsEmpty()
        {
            Node n = new Node(0);
            n.RemoveItems();
            Assert.AreEqual(0, n.get_Items().Count);
        }

        //Check if all items are removed upon pickup
        [TestMethod]
        public void RemoveItems()
        {
            Node n = new Node(0);
            n.Add_Item(new Health_Potion());
            n.Add_Item(new Time_Crystal());
            n.Add_Item(new Magic_Scroll());
            n.RemoveItems();
            Assert.AreEqual(0, n.get_Items().Count);
        }

        //Check if the node has no items when initialized
        [TestMethod]
        public void NoPacks()
        {
            Node n = new Node(0);
            Assert.IsFalse(n.hasPack());
        }

        //Check if adding an items changes the outcome 
        [TestMethod]
        public void AddPack()
        {
            Node n = new Node(0);
            n.AddPack(true);
            Assert.IsTrue(n.hasPack());
        }

        //Check if pushing a pack in an empty node works
        [TestMethod]
        public void PushPack()
        {
            Node n = new Node(0);
            n.pushPack(new Pack(0));
            Assert.IsTrue(n.hasPack());
        }

        //Check if capacity isnt full when it shouldnt be
        [TestMethod]
        public void CapacityNotFull()
        {
            Node n = new Node(0);
            n.AddPack(true);
            Assert.IsTrue(n.AddPack(true));
        }

        //Check if capacity if full when it should be
        [TestMethod]
        public void CapacityFull()
        {
            Node n = new Node(0);
            n.AddPack(true);
            n.pushPack(new Pack(0));
            n.pushPack(new Pack(0));
            Assert.IsFalse(n.AddPack(true));
        }

        //Check if a node with no monsters yields the right some of monsters
        [TestMethod]
        public void NoMonsters()
        {
            Node n = new Node(1);
            Assert.AreEqual(0, n.TotalMonsters());
        }

        //Check if a node one pack yields in the right number of monsters (3)
        [TestMethod]
        public void OnePack()
        {
            Node n = new Node(1);
            n.AddPack(true);
            Assert.AreEqual(3, n.TotalMonsters());
        }

        //Check max capacity
        [TestMethod]
        public void MaxCapacity()
        {
            Node n = new Node(1);
            n.AddPack(true);
            n.AddPack(true);
            n.AddPack(true);
            Assert.AreEqual(9, n.maxCap());
            Assert.AreEqual(9, n.TotalMonsters());
        }

        //Check if capacity contraint can be broken
        [TestMethod]
        public void MaxCapacityRestriction()
        {
            Node n = new Node(1);
            n.AddPack(true);
            n.AddPack(true);
            n.AddPack(true);
            n.AddPack(true);
            Assert.AreEqual(9, n.TotalMonsters());
        }

        //Check if adding a neighbour works
        [TestMethod]
        public void AddNeighbourNotFull()
        {
            Node n = new Node(1);
            n.AddNeighbour(2);
            Assert.IsTrue(n.IsNeighbour(2));
        }

        //Check if adding a neighbour works correctly if the node already has four neighbours
        [TestMethod]
        public void AddNeighbourFull()
        {
            Node n = new Node(1, new int[] { 2, 3, 4, 5 });
            n.AddNeighbour(6);
            Assert.IsFalse(n.IsNeighbour(6));
        }

        //Check if node doesnt correctly assume some other node is a neighbour when it isnt.
        [TestMethod]
        public void IsNeighbourNoNeighbours()
        {
            Node n = new Node(1);
            Assert.IsFalse(n.IsNeighbour(7));
        }

        //Check if the node actually sees neighbours as neighbours
        [TestMethod]
        public void IsNeighbour()
        {
            Node n = new Node(1, new int[] { 2, 3, 4, 5 });
            Assert.IsTrue(n.IsNeighbour(2));
            Assert.IsTrue(n.IsNeighbour(3));
            Assert.IsTrue(n.IsNeighbour(4));
            Assert.IsTrue(n.IsNeighbour(5));
        }

        //Check if node gives a null if there are no neighbours
        [TestMethod]
        public void GetNeighboursNoNeighbours()
        {
            Node n = new Node(0);
            Assert.IsNull(n.GetNeighbours());
        }

        //Check if right neighbours are returned upon requesting them
        [TestMethod]
        public void GetNeighbours()
        {
            Node n = new Node(1, new int[] { 2, 3, 4, 5 });
            int[] k = n.GetNeighbours();
            Assert.AreEqual(4, k.Length);
            for (int i = 0; i < 4; i++)
                Assert.IsTrue(n.IsNeighbour(k[i]));
        }

        //Check if node is full when it actually should be empty
        [TestMethod]
        public void IsFullEmpty()
        {
            Node n = new Node(2);
            Assert.IsFalse(n.IsFull());
        }

        //Check if node is full when it shouldnt be
        [TestMethod]
        public void IsFullFilled()
        {
            Node n = new Node(0);
            n.AddNeighbour(1);
            Assert.IsFalse(n.IsFull());
        }

        //Check if node is full when it actually should be
        [TestMethod]
        public void IsFull()
        {
            Node n = new Node(0, new int[] { 1, 2, 3, 4 });
            Assert.IsTrue(n.IsFull());
        }
    
        //Check if removing a neighbour from a node with no neighbour works still yields 0 neighbours
        [TestMethod]
        public void RemoveNeighbourEmpty()
        {
            Node n = new Node(0);
            Assert.IsFalse(n.RemoveNeighbour(6));
            Assert.AreEqual(0, n.NumNeighbours);
        }

        //Check if removing works correctly if a valid neighbour is removed
        [TestMethod]
        public void RemoveNeighbourValid()
        {
            Node n = new Node(0);
            n.AddNeighbour(1);
            Assert.IsTrue(n.RemoveNeighbour(1));
            Assert.AreEqual(0, n.NumNeighbours);
        }

        //Check if neighbours arent removed when trying to remove a node that isnt a neighbour
        [TestMethod]
        public void RemoveNeighBourInvalid()
        {
            Node n = new Node(0);
            n.AddNeighbour(1);
            Assert.IsFalse(n.RemoveNeighbour(3));
            Assert.AreEqual(1, n.NumNeighbours);
        }
    
        //Check if sum of health of monsters equals 0 in an empty node
        [TestMethod]
        public void SumMonstersEmpty()
        {
            Node n = new Node(0);
            Assert.AreEqual(0, n.SumMonsterHealth());
        }

        //Check if sum of health is correctly calculated when some monsters are damaged
        [TestMethod]
        public void SumMonstersDamaged()
        {
            Node n = new Node(0);
            n.AddPack(true);
            int init = n.SumMonsterHealth();
            Pack p = n.popPack();
            p.hit_pack(10);
            n.pushPack(p);
            int end = n.SumMonsterHealth();
            Assert.AreEqual(10, init - end);
        }

        //Check if sum of health is correct upon spawning a fresh pack of monsters
        [TestMethod]
        public void SumMonstersFullHealth()
        {
            Node n = new Node(0);
            Pack p = new Pack(0);
            n.pushPack(p);
            Assert.AreEqual(p.GetPackHealth(), n.SumMonsterHealth());
        }

        //Same tests as above for pots
        [TestMethod]
        public void SumHealEmpty()
        {
            Node n = new Node(0);
            Assert.AreEqual(0, n.SumHealPots());
        }

        [TestMethod]
        public void SumHealNoHeal()
        {
            Node n = new Node(0);
            for(int i = 0; i < 20; i++)
            {
                if(i % 2 == 1) n.Add_Item(new Magic_Scroll());
                else n.Add_Item(new Time_Crystal());
            }
            Assert.AreEqual(0, n.SumHealPots());
        }

        //Check total heal amount when mixed items are present on a node
        [TestMethod]
        public void SumHeal()
        {
            Node n = new Node(0);
            for(int i = 0; i < 15; i++)
            {
                if(i % 3 == 1) n.Add_Item(new Magic_Scroll());
                else if(i % 3 == 2) n.Add_Item(new Health_Potion());
                else n.Add_Item(new Time_Crystal());
            }
            Assert.AreEqual(5 * 25, n.SumHealPots());
        }

        [TestMethod]
        public void TToStringNoNeigh()
        {
            Node n = new Node(0);
            string s = "Node 0 ";
            Assert.AreEqual(s, n.ToString());
        }

        [TestMethod]
        public void TToString()
        {
            Node n = new Node(0, new int[] { 1, 2, 3, 4 });
            string s = "Node 0 1 2 3 4 ";
            Assert.AreEqual(s, n.ToString());
        }

        //Check if checking if the node is a bridge yields false when it isnt a bridge
        [TestMethod]
        public void InvalidBridge()
        {
            Node n = new Node(35);
            Assert.IsFalse(n.IsBridge(6));
        }

        //Check if checking for a bridge returns true if it actually is a bridge
        [TestMethod]
        public void IsValidBridge()
        {
            Node n = new Node(35);
            Assert.IsTrue(n.IsBridge(5));
        }

        //Check if monsters want to retreat when they shouldnt
        [TestMethod]
        public void NoRetreat()
        {
            Node n = new Node(901);
            n.AddPack(true);
            n.AddPack(true);
            Assert.IsFalse(n.Retreat());
            Assert.AreEqual(6, n.TotalMonsters());
        }

        //Check if monsters want to retreat when they should
        [TestMethod]
        public void Retreat()
        {
            Node n = new Node(901);
            n.AddPack(true);
            n.AddPack(true);
            Pack p = n.popPack();
            p.hit_pack(45);
            p.hit_pack(45);
            p.hit_pack(44);
            n.pushPack(p);
            Assert.IsTrue(n.Retreat());
        }
    
        //Check if adding a neighbour that is already present doesnt yield a duplicate entry
        [TestMethod]
        public void AddNeighbourAlreadyNeighbour()
        {
            Node n = new Node(0);
            n.AddNeighbour(3);
            n.AddNeighbour(3);
            Assert.AreEqual(1, n.NumNeighbours);
        }
    }
}
