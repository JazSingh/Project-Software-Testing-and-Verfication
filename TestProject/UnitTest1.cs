using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class MonsterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            HPTest();
            damageTest();
        }

        [TestMethod]
        public void TestMethod2()
        {

        }

        public void HPTest()
        {
            Monster m = new Monster();
            int expected = 15;
            int actual = m.GetHP();
            Assert.AreEqual(expected, actual);
        }

        public void damageTest()
        {
            Monster target = new Monster();
            int expected = 3;
            int actual = target.hits();
            Assert.AreEqual(expected, actual);
        }
    }
}
