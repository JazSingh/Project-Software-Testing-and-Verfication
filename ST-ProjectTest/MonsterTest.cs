using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_ProjectTest
{
    class MonsterTest
    {
    private TestContext testContextInstance;
    
        //[ClassInitialize()]
        public MonsterTest()
        {
            HPTest();
            damageTest();
        }     
        //[ClassCleanup()]
        //public static void MyClassCleanup() ...
        //[TestInitialize()]
        //public void MyTestInitialize() ...
        //[TestCleanup()]
        //public void MyTestCleanup() ...

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
         //[TestMethod()]
         public void getsHitTest()
         {}
        //[TestMethod()] 
        public void hitsTest()
         { }

    }
}
