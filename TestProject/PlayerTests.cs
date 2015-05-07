using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ST_Project;

namespace TestProject
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void GetHP()
        {
            Player p = new Player();
            int expected = 250;
            int actual = p.GetHP();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void isAlive()
        {
            Player p = new Player();
            bool expected = true;
            bool actual = p.IsAlive();
            Assert.AreEqual(expected, actual);
        }
    }
}
