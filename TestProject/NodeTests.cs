using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NoNeighboursLength()
        {
            Node n = new Node(0);
            Assert.AreEqual(n.NumNeighbours, 0);
        }

        [TestMethod]
        public void SomeNeighboursLength()
        {
            Node n = new Node(1, new int[] { 2, 3, 4 });
            Assert.AreEqual(3, n.NumNeighbours);
        }

        [TestMethod]
        public void AdjLength()
        {
            for (int i = 0; i < 20; i++)
            {
                Node n = new Node(i);
                Assert.AreEqual(4, n.get_Adj().Length);
                Assert.AreEqual(4, n.getadj().Length);
            }
        }

        [TestMethod]
        public void CapacityNormal()
        {
            Node n = new Node(3);
            Assert.AreEqual(9, n.GetCapacity());
        }

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

        [TestMethod]
        public void BridgeStartNode()
        {
            Node n = new Node(0);
            for (int i = 4; i < 7; i++)
                Assert.IsFalse(n.IsBridge(i));
        }

        [TestMethod]
        public void GetIdentifier()
        {
            int k = 12;
            Node n = new Node(12);
            Assert.AreEqual(k, n.ID);
        }

        [TestMethod]
        public void NoItems()
        {
            Node n = new Node(1);
            Assert.AreEqual(0, n.get_Items().Count);
        }

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

        [TestMethod]
        public void RemoveItemsEmpty()
        {
            Node n = new Node(0);
            n.RemoveItems();
            Assert.AreEqual(0, n.get_Items().Count);
        }

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

        [TestMethod]
        public void NoPacks()
        {
            Node n = new Node(0);
            Assert.IsFalse(n.hasPack());
        }

        [TestMethod]
        public void AddPack()
        {
            Node n = new Node(0);
            n.AddPack();
            Assert.IsTrue(n.hasPack());
        }

        [TestMethod]
        public void PushPack()
        {
            Node n = new Node(0);
            n.pushPack(new Pack(0));
            Assert.IsTrue(n.hasPack());
        }

        [TestMethod]
        public void CapacityNotFull()
        {
            Node n = new Node(0);
            n.AddPack();
            Assert.IsTrue(n.AddPack());
        }

        [TestMethod]
        public void CapacityFull()
        {
            Node n = new Node(0);
            n.AddPack();
            n.pushPack(new Pack(0));
            n.pushPack(new Pack(0));
            Assert.IsFalse(n.AddPack());
        }

        [TestMethod]
        public void NoMonsters()
        {
            Node n = new Node(1);
            Assert.AreEqual(0, n.TotalMonsters());
        }

        [TestMethod]
        public void OnePack()
        {
            Node n = new Node(1);
            n.AddPack();
            Assert.AreEqual(3, n.TotalMonsters());
        }

        [TestMethod]
        public void MaxCapacity()
        {
            Node n = new Node(1);
            n.AddPack();
            n.AddPack();
            n.AddPack();
            Assert.AreEqual(9, n.maxCap());
            Assert.AreEqual(9, n.TotalMonsters());
        }

        [TestMethod]
        public void MaxCapacityRestriction()
        {
            Node n = new Node(1);
            n.AddPack();
            n.AddPack();
            n.AddPack();
            n.AddPack();
            Assert.AreEqual(9, n.TotalMonsters());
        }

        [TestMethod]
        public void AddNeighbourNotFull()
        {
            Node n = new Node(1);
            n.AddNeighbour(2);
            Assert.IsTrue(n.IsNeighbour(2));
        }

        [TestMethod]
        public void AddNeighbourFull()
        {
            Node n = new Node(1, new int[] { 2, 3, 4, 5 });
            n.AddNeighbour(6);
            Assert.IsFalse(n.IsNeighbour(6));
        }

        [TestMethod]
        public void IsNeighbourNoNeighbours()
        {
            Node n = new Node(1);
            Assert.IsFalse(n.IsNeighbour(7));
        }

        [TestMethod]
        public void IsNeighbour()
        {
            Node n = new Node(1, new int[] { 2, 3, 4, 5 });
            Assert.IsTrue(n.IsNeighbour(2));
            Assert.IsTrue(n.IsNeighbour(3));
            Assert.IsTrue(n.IsNeighbour(4));
            Assert.IsTrue(n.IsNeighbour(5));
        }

        [TestMethod]
        public void GetNeighboursNoNeighbours()
        {
            Node n = new Node(0);
            Assert.IsNull(n.GetNeighbours());
        }

        [TestMethod]
        public void GetNeighbours()
        {
            Node n = new Node(1, new int[] { 2, 3, 4, 5 });
            int[] k = n.GetNeighbours();
            Assert.AreEqual(4, k.Length);
            for (int i = 0; i < 4; i++)
                Assert.IsTrue(n.IsNeighbour(k[i]));
        }

        [TestMethod]
        public void IsFullEmpty()
        {
            Node n = new Node(2);
            Assert.IsFalse(n.IsFull());
        }

        [TestMethod]
        public void IsFullFilled()
        {
            Node n = new Node(0);
            n.AddNeighbour(1);
            Assert.IsFalse(n.IsFull());
        }

        [TestMethod]
        public void IsFull()
        {
            Node n = new Node(0, new int[] { 1, 2, 3, 4 });
            Assert.IsTrue(n.IsFull());
        }
    
        [TestMethod]
        public void RemoveNeighbourEmpty()
        {
            Node n = new Node(0);
            Assert.IsFalse(n.RemoveNeighbour(6));
            Assert.AreEqual(0, n.NumNeighbours);
        }

        [TestMethod]
        public void RemoveNeighbourValid()
        {
            Node n = new Node(0);
            n.AddNeighbour(1);
            Assert.IsTrue(n.RemoveNeighbour(1));
            Assert.AreEqual(0, n.NumNeighbours);
        }

        [TestMethod]
        public void RemoveNeighBourInvalid()
        {
            Node n = new Node(0);
            n.AddNeighbour(1);
            Assert.IsFalse(n.RemoveNeighbour(3));
            Assert.AreEqual(1, n.NumNeighbours);
        }
    
        [TestMethod]
        public void SumMonstersEmpty()
        {
            Node n = new Node(0);
            Assert.AreEqual(0, n.SumMonsterHealth());
        }

        [TestMethod]
        public void SumMonstersDamaged()
        {
            Node n = new Node(0);
            n.AddPack();
            int init = n.SumMonsterHealth();
            Pack p = n.popPack();
            p.hit_pack(10);
            n.pushPack(p);
            int end = n.SumMonsterHealth();
            Assert.AreEqual(10, init - end);
        }

        [TestMethod]
        public void SumMonstersFullHealth()
        {
            Node n = new Node(0);
            Pack p = new Pack(0);
            n.pushPack(p);
            Assert.AreEqual(p.GetPackHealth(), n.SumMonsterHealth());
        }

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
        public void ToStringNoNeigh()
        {
            Node n = new Node(0);
            string s = "Node 0 ";
            Assert.AreEqual(s, n.ToString());
        }

        [TestMethod]
        public void ToString()
        {
            Node n = new Node(0, new int[] { 1, 2, 3, 4 });
            string s = "Node 0 1 2 3 4 ";
            Assert.AreEqual(s, n.ToString());
        }

        [TestMethod]
        public void InvalidBridge()
        {
            Node n = new Node(35);
            Assert.IsFalse(n.IsBridge(6));
        }

        [TestMethod]
        public void IsValidBridge()
        {
            Node n = new Node(35);
            Assert.IsTrue(n.IsBridge(5));
        }

        [TestMethod]
        public void NoRetreat()
        {
            Node n = new Node(901);
            n.AddPack();
            n.AddPack();
            Assert.IsFalse(n.Retreat());
            Assert.AreEqual(6, n.TotalMonsters());
        }

        [TestMethod]
        public void Retreat()
        {
            Node n = new Node(901);
            n.AddPack();
            n.AddPack();
            Pack p = n.popPack();
            p.hit_pack(45);
            p.hit_pack(45);
            p.hit_pack(44);
            n.pushPack(p);
            Assert.IsTrue(n.Retreat());
        }
    
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
