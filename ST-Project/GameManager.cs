using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{

    class GameManager
    {
        GameState state;
        Hoofdscherm hoofdScherm;

        public GameManager(int i, Hoofdscherm h)
        {
            state = new GameState(i);
            hoofdScherm = h;
        }

        /*public void MoveToNode(int t)
        {
            int i = state.get_player().get_position();
            if (i != t)
            {
                state.getDungeon().nodes[i].getadj();
            }
        }*/

        public void Attack(Pack p)
        {

        }

        public void Save()
        {
            
            string filename;

        }
    }
}
