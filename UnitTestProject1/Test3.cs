using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class Test3
    {
        // 2. Orders (Hunt and Defend) are handled correctly:

        [TestMethod]
        public void Test_A()
        {
            // constraint: "Orders are indeed issued when situations that should trigger them occur."
        }

        [TestMethod]
        public void Test_B()
        {
            // constraint: “If there is no situation that prevents a monster pack 
            // to finish its order (it didn’t eliminated along the way, it is not 
            // continously blocked by a full node), then it will reach its destination node, 
            // as specified by the order.”
        }
    }
}
