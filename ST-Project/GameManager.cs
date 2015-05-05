using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void MoveToNode(int t)
        {
            int i = state.GetPlayer().get_position();
            bool buur = false;

            if (i != t)
            {
                int[] buren = state.GetDungeon().nodes[i].getadj();
                for (int s = 0;s<buren.Length;s++)
                {
                    if (buren[s] == t)
                        buur = true;
                }

                if (buur)
                {
                    state.set_position(t);
                }
            }
        }

        //Methods called from View
        public void PlayerMoved(int newNode)
        {
            
        }

        //Hoofdscherm diff select
        public void DiffSelectNotify(int diff)
        {
            state = new GameState(diff);
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
    }
}
