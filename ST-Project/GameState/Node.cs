using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project.GameState
{
    class Node
    {
        int identifier;
        int[] adj;
        int numNeighbours;
        Stack<Pack> packs;

        public Node(int i)
        {
            identifier = i;
            adj = new int[4];
            numNeighbours = 0;
            packs = new Stack<Pack>();
        }

        public void AddNeighbour(int node)
        {
            adj[numNeighbours++] = node; 
        }

        public int ID
        {
            get { return identifier; }
        }

        public override string ToString()
        {
            string s = identifier.ToString() + ": ";
            for (int i = 0; i < numNeighbours; i++)
                s += adj[i] + " ";
            return s;
        }
    }
}
