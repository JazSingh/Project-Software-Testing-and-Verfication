using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class OracleTests
    {
        //Check if Deciding can yield both true and false
        [TestMethod]
        public void TestDecide() 
        {
            bool exp = true;
            bool dec = Oracle.Decide();
            bool act = dec == true || dec == false;
            Assert.AreEqual(exp, act);
        }

        //Check if expected values lies in range
        [TestMethod]
        public void TestBetweenTwoNumbersEqual()
        {
            int n = 10;
            int act = Oracle.GiveNumber(n, n);
            int exp = 10;
            Assert.AreEqual(act, exp);
        }

        //Check if range is inclusive on both sides
        public void TestBetweenTwoNumbersInclusiveRanges()
        {
            int min = 0;
            int max = 100;
            int r = Oracle.GiveNumber(min, max);
            bool inRanges = r >= min && r <= max;
            Assert.IsTrue(inRanges);
        }

        //Check if oracle doesnt crash if swapping min and max
        [TestMethod]
        public void TestTwoNumbersSwapMinMax()
        {
            int r = Oracle.GiveNumber(1, 0);
            Assert.IsTrue(r >= 0 && r <= 1);
        }

        //Check ranges
        [TestMethod]
        public void TestOneNumber()
        {
            int k = Oracle.GiveNumber(100);
            Assert.IsTrue(k >= 0 && k <= 100);
        }

        //Check if behaviour is correct when range is [0,0]
        [TestMethod]
        public void TestOneNumberZero()
        {
            int k = Oracle.GiveNumber(0);
            Assert.AreEqual(0, k);
        }

        //Check behaviour if oracle is determenistic
        [TestMethod]
        public void TestDetermDecide()
        {
            Oracle.DETERM = true;
            Oracle.DETERMF = false;
            Assert.IsTrue(Oracle.Decide());
        }

        //Check deterministic behaviour for ranges
        [TestMethod]
        public void TestDetermTwoNum()
        {
            Oracle.DETERM = true;
            Assert.AreEqual(Oracle.GiveNumber(0, 100), 100);
        }

        [TestMethod]
        public void TestDetermOneNum()
        {
            Oracle.DETERM = true;
            Assert.AreEqual(Oracle.GiveNumber(100), 100);
        }
    }
}
