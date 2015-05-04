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

        /*public void MoveToNode(int t)
        {
            int i = state.get_player().get_position();
            if (i != t)
            {
                state.getDungeon().nodes[i].getadj();
            }
        }*/

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
