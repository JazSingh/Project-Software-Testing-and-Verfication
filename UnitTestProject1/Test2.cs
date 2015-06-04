using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST_Project;
using System.Collections.Generic;


namespace UnitTestProject1
{
    [TestClass]
    public class Test2
    {
        [TestMethod]
        public void Test2_hs_1()
        {
            //player Tester gets highscore with 999 points, highscores are still descending, Tester is in the highscore list
            DeleteHS();
            CreateHS_Descending();

            Replayer z = new Replayer("2_1a.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            string[] hscr = System.IO.File.ReadAllLines("highscores.txt");
            bool foundTester = false;
            if (hscr[0].Split(' ')[0] == "Tester")
                foundTester = true;

            for (int i = 0; i < 9; i++)
            {
                int scr1 = Convert.ToInt32(hscr[i].Split(' ')[1]);
                int scr2 = Convert.ToInt32(hscr[i + 1].Split(' ')[1]);
                bool expected = true;
                bool actual = scr1 >= scr2;
                Assert.AreEqual(expected, actual);

                if (hscr[i + 1].Split(' ')[0] == "Tester")
                    foundTester = true;
            }
            Assert.AreEqual(true, foundTester);
        }

        [TestMethod]
        public void Test2_hs_2()
        {
            //Highscores are empty, player Tester gets first place with 999 points
            DeleteHS();
            CreateHS_All0();

            Replayer z = new Replayer("2_1a.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            string[] hscr = System.IO.File.ReadAllLines("highscores.txt");
            string expected = "Tester 999";
            string expected2 = "Empty 0";
            string actual = hscr[0];

            Assert.AreEqual(expected, actual);
            for (int i = 1; i < 10; i++)
            {
                actual = hscr[i];
                Assert.AreEqual(expected2, actual);
            }
        }

        [TestMethod]
        public void Test2_hs_3()
        {
            //Highscores are descending with Pro0 t/m Pro9, Tester gets place 10 and removes pro9 from the list 
            DeleteHS();
            CreateHS_Descending();

            Replayer z = new Replayer("2_1b.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            string[] hscr = System.IO.File.ReadAllLines("highscores.txt");
            string expected = "Tester 345";
            string actual = hscr[9];

            Assert.AreEqual(expected, actual);
            for (int i = 0; i < 9; i++)
            {
                expected = "Pro" + i;
                actual = hscr[i].Split(' ')[0];
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Test2_hs_4()
        {
            //no highscore, Highscores are full with score 999 
            DeleteHS();
            CreateHS_All999();

            Replayer z = new Replayer("2_1b.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            string[] hscr = System.IO.File.ReadAllLines("highscores.txt");
            string expected = "Pro 999";

            for (int i = 0; i < 9; i++)
            {
                string actual = hscr[i];
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Test2_hs_5()
        {
            //Tester has score of 999, Highscores are full with score 999 
            DeleteHS();
            CreateHS_All999();

            Replayer z = new Replayer("2_1c.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            string[] hscr = System.IO.File.ReadAllLines("highscores.txt");
            string expected = "Pro 999";

            for (int i = 0; i < 9; i++)
            {
                string actual = hscr[i];
                Assert.AreEqual(expected, actual);
            }
        }

        public void DeleteHS()
        {
            if (System.IO.File.Exists("highscores.txt"))
            {
               System.IO.File.Delete("highscores.txt");
            }
        }

        public void CreateHS_All0()
        {
            System.IO.File.Create("highscores.txt").Close();
               
            using (System.IO.StreamWriter sw = System.IO.File.AppendText("highscores.txt"))
            {
                for (int i = 0; i < 10; i++ )
                    sw.WriteLine("Empty 0");
            }
        }

        public void CreateHS_All999()
        {
            System.IO.File.Create("highscores.txt").Close();

            using (System.IO.StreamWriter sw = System.IO.File.AppendText("highscores.txt"))
            {
                for (int i = 0; i < 10; i++)
                    sw.WriteLine("Pro 999");
            }
        }

        public void CreateHS_Descending()
        {
            System.IO.File.Create("highscores.txt").Close();
            using (System.IO.StreamWriter sw = System.IO.File.AppendText("highscores.txt"))
            {
                int score = 1234;
                for (int i = 0; i < 10; i++)
                {
                    sw.WriteLine("Pro" + i + " " + score);
                    score -= 111;
                } 
            }
        }

        [TestMethod]
        public void Test2_sc_a()
        {
            //null node score
            DeleteHS();
            CreateHS_Descending();

            Replayer z = new Replayer("2_2a.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            int expected = 9;
            int actual = z.QueryState().GetPlayer().getScore();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test2_sc_b()
        {
            //non bridge score
            DeleteHS();
            CreateHS_Descending();

            Replayer z = new Replayer("2_2b.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            int expected = 9;
            int actual = z.QueryState().GetPlayer().getScore();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test2_sc_c()
        {
            //bridge lvl 1
            DeleteHS();
            CreateHS_Descending();

            Replayer z = new Replayer("2_2c.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            int expected = 9;
            int actual = z.QueryState().GetPlayer().getScore();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test2_sc_d()
        {
            //bridge lvl 2
            DeleteHS();
            CreateHS_Descending();

            Replayer z = new Replayer("2_2d.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            int expected = 18;
            int actual = z.QueryState().GetPlayer().getScore();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test2_sc_e()
        {
            //bridge lvl 2, player has already 9 points
            DeleteHS();
            CreateHS_Descending();

            Replayer z = new Replayer("2_2e.txt");
            z.Init();
            while (z.HasNext())
            {
                z.Step();
            }
            int expected = 27;
            int actual = z.QueryState().GetPlayer().getScore();

            Assert.AreEqual(expected, actual);
        }
    }
}
