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

        public GameManager(int i)
        {
            state = new GameState(i);
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

        public void Attack(Pack p)
        {

        }
    }
}
