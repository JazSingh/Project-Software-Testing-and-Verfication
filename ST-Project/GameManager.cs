using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ST_Project
{

    public class GameManager
    {
        public GameState state;
        public Gamescherm gs;
        public Hoofdscherm hs;

        public GameManager()
        {
            Hoofdscherm hs = new Hoofdscherm(this);
            hs.Show();
        }

        //Methods called from View
        public void NotifyFinished()
        {
            if (state.GetDungeon().difficulty == 5)
            {
                gs.ShowUitgespeeld();
                gs.Close();
                hs = new Hoofdscherm(this);
                hs.Show();
                return;
            }

            if (!gs.Save())
                state.NextLevel();
            gs.Close();
            gs = new Gamescherm(state.GetDungeon().difficulty, this);
            state.SetPosition(0);
            gs.Show();
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
                int[] buren = state.GetDungeon().nodes[i].getadj();
                for (int s = 0; s < buren.Length; s++)
                {
                    if (buren[s] == newNode)
                        buur = true;
                }

                if (buur)
                {
                    state.SetPosition(newNode);
                    state.UpdateTime();
                }
            }
            gs.Invalidate();
        }

        public bool Fight()
        {
            if (state.Fight())
            {
                return true;
            }
            return false;
        }

        //Hoofdscherm diff select
        public void DiffSelectNotify(int diff)
        {
            state = new GameState(diff);
            gs = new Gamescherm(diff, this);
            gs.Show();
            gs.Invalidate();
        }

        public void GameLoadNotify(GameState st, int diff)
        {
            state = st;
            gs = new Gamescherm(diff, this);
            gs.Show();
            gs.Invalidate();
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


        public void UseScroll()
        {
            state.UseScroll();
        }
    }
}
