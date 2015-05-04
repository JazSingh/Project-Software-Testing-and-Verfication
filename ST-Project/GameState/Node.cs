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
        public int[] adj;
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
            if(!IsNeighbour(node) && numNeighbours < 4)
                adj[numNeighbours++] = node; 
        }

        public bool IsNeighbour(int node)
        {
            for(int i = 0; i < numNeighbours; i++)
                if(adj[i] == node) return true;
            return false;
        }

        public int[] GetNeighbours()
        {
            if(numNeighbours == 0) return null;
            int[] neighs = new int[numNeighbours];
            for (int i = 0; i < numNeighbours; i++)
                neighs[i] = adj[i];
            return neighs;
        }

        public bool IsFull()
        {
            return numNeighbours == 4;
        }

        public bool RemoveNeighbour(int v)
        {
            int index = -1;
            for (int i = 0; i < numNeighbours; i++)
                if (adj[i] == v) index = i;
            if (index == -1) return false;

            adj[index] = -1;
            for (int i = index; i < numNeighbours; i++)
                adj[i] = adj[i + 1];
            numNeighbours--;
            return true;
        }

        public int ID
        {
            get { return identifier; }
        }

        public int NumNeighbours
        {
            get { return numNeighbours; }
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
