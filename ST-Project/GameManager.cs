using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ST_Project
{
    //controller
    public class GameManager
    {
        private GameState state;
        private Gamescherm gs;
        public Hoofdscherm hs;
        private bool logging;
        string logpath;

        public GameManager()
        {
            logging = false;
            Hoofdscherm hs = new Hoofdscherm(this);
            hs.Show();
        }

        public void Logging()
        {
            logging = !logging;

            if (logging)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = ".txt";
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    logpath = sfd.FileName;
            }
                else
                {
                    logpath = "log.txt";
        }
            }
        }

        //Methods called from View
        public void NotifyFinished()
        {
            if (!gs.Save())
                state.NextLevel();
            gs.Close();
            gs = new Gamescherm(state.GetDungeon().difficulty, this);
            state.SetPosition(0);
            gs.Show();

            if (logging)
            {
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    sw.WriteLine("End node");
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = ".txt";
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    logpath = sfd.FileName;

                    string[] dungeon = state.GetDungeon().ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    string[] player = state.GetPlayer().ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                    using (StreamWriter sw = File.AppendText(logpath))
                    {
                        foreach (string line in dungeon)
                            sw.WriteLine(line);
                        foreach (string line in player)
                            sw.WriteLine(line);
                        sw.WriteLine("ACTIONS");
                    }
                }
            }
        }

        public void Save(string filename)
        {
            state.Save(filename);
        }

        public void PlayerMoved(int newNode)
        {
            int i = state.GetPlayer().get_position();
            bool buur = false;

            if (i != newNode)
            {
                int[] buren = state.GetDungeon().GetNode(i).get_Adj();
                for (int s = 0; s < buren.Length; s++)
                {
                    if (buren[s] == newNode)
                        buur = true;
                }

                if (buur)
                {
                    state.SetPosition(newNode);
                    state.UpdateTime();
                    state.PackMoves();

                    if (logging)
                    {
                        using (StreamWriter sw = File.AppendText(logpath))
                        {
                            sw.WriteLine("Moving to " + newNode);
                        }
                    }
                }
            }
            gs.Invalidate();
        }

        public bool Fight()
        {
            state.UpdateLKP();
            if (logging)
            {
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    sw.WriteLine("Fighting");
                }
            }

            state.PackMoves();
            if (state.Fight())
            {
                if (state.PlayerDead())
                    gs.GameOver();
                return true;
            }
            if (state.PlayerDead())
                gs.GameOver();
            return false;
        }

        public void SetHighScore(int name)
        {
            int sc = state.GetPlayer().getScore();
        }

        public int NewHighscore()
        {
            int sc = state.GetPlayer().getScore();
            Tuple<string, int>[] highs = ReadHighscores();
            for (int i = 0; i < 10; i++)
                if (highs[i].Item2 < sc)
                    return i;
            return -1;
        }

        public void WriteHighscore(string name)
        {
            int sc = state.GetPlayer().getScore();
            Tuple<string, int> newhs = new Tuple<string, int>(name, sc);
            int index = NewHighscore();
            Tuple<string, int>[] hss = ReadHighscores();
            //hss[index] = newhs;
            int i = hss.Length;
            while(i - index > 0)
            {
                hss[i] = hss[i - 1];
                i--;
            }
            hss[index] = newhs;

            WriteHighscoresToFile(hss);
            gs.Close();
            hs = new Hoofdscherm(this);
            hs.Show();
        }

        public void WriteHighscoresToFile(Tuple<string, int>[] hs)
        {
            string k = string.Empty;

            for (int i = 0; i < 10; i++)
                k += string.Format("{0} {1}{2}", hs[i].Item1, hs[i].Item2, Environment.NewLine);

            File.WriteAllText("highscores.txt", k);
        }

        public void SetHighScore()
        {
            gs.Close();
            NewHighscore nhs = new NewHighscore(this);
            nhs.Show();
        }

        //Hoofdscherm diff select
        public void DiffSelectNotify(int diff)
        {
            state = new GameState(diff);
            gs = new Gamescherm(diff, this);
            gs.Show();
            gs.Invalidate();

            LoggingSetup();
        }

        public void GameLoadNotify(GameState st, int diff)
        {
            state = st;
            gs = new Gamescherm(diff, this);
            gs.Show();
            gs.Invalidate();

            LoggingSetup();
        }

        private void LoggingSetup()
        {
            if (logging)
            {
                string[] dungeon = state.GetDungeon().ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string[] player = state.GetPlayer().ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                using (StreamWriter sw = File.AppendText(logpath))
                {
                    foreach (string line in dungeon)
                        sw.WriteLine(line);
                    foreach (string line in player)
                        sw.WriteLine(line);
                    sw.WriteLine("ACTIONS");
                }
            }
        }

        //Convience methods
        public Dungeon GetDungeon()
        {
            return state.GetDungeon();
        }

        public GameState GetState()
        {
            return state;
        }

        public Player GetPlayer()
        {
            return state.GetPlayer();
        }

        private Tuple<string, int>[] ReadHighscores()
        {
            Tuple<string, int>[] scores = new Tuple<string, int>[10];

            if (!File.Exists("highscores.txt"))
            {
                string[] contents = new string[10];
                for (int i = 0; i < 10; i++)
                    contents[i] = "Empty 0";

                File.WriteAllLines("highscores.txt", contents);
            }

            string[] lines = File.ReadAllLines("highscores.txt");

            for (int i = 0; i < 10; i++)
            {
                string[] split = lines[i].Split();
                int score = int.Parse(split[split.Length - 1]);
                string name = string.Empty;
                for (int j = 0; j < split.Length - 1; j++)
                    name += split[j];
                scores[i] = new Tuple<string, int>(name, score);
            }

            return scores;
        }

        public void ShowHighScores()
        {
            Highscore hsc = new Highscore(this, ReadHighscores());
            hsc.Show();
            hsc.Invalidate();
        }


        public void UsePotion()
        {
            state.UsePotion();
            state.UpdateLKP();

            if (logging)
            {
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    sw.WriteLine("using potion");
                }
            }
        }

        public void UseCrystal()
        {
            state.UpdateLKP();
            state.UseCrystal();

            if (logging)
            {
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    sw.WriteLine("using crystal");
                }
            }
        }

        public void UseScroll()
        {
            state.UpdateLKP();
            if (state.UseScroll())
            {
                if (logging)
                {
                    using (StreamWriter sw = File.AppendText(logpath))
                    {
                        sw.WriteLine("using scroll, node explodes");
                    }
                }

                if (state.CheckFinished())
                    NotifyFinished();
                else
                {
                    //gs.locations =  new Dictionary<int, Tuple<int, int>>();
                    gs.Invalidate();
                }
            }
            else
            {
                if (logging)
                {
                    using (StreamWriter sw = File.AppendText(logpath))
                    {
                        sw.WriteLine("using scroll");
                    }
                }
            }
        }
    }
}
