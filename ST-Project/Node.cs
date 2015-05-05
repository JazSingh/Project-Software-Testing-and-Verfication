using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    public class Node
    {
        int identifier;
        int[] adj;
        int numNeighbours;
        Stack<Pack> packs;
        List<Item> items;

        public Node(int i)
        {
            identifier = i;
            adj = new int[4];
            numNeighbours = 0;
            packs = new Stack<Pack>();
            items = new List<Item>();
        }
        public int[] get_Adj()
        {
            return adj;
        }

        public void Add_Item(Item i)
        {
            items.Add(i);
        }

        public List<Item> get_Items()
        {
            return items;
        }

        public int Amount_of_packs()
        {
            return packs.Count;
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
            string s = "Node " + identifier.ToString() + ": ";
            for (int i = 0; i < numNeighbours; i++)
                s += adj[i] + " ";
            s += Environment.NewLine + "Packs:" + Environment.NewLine;
            foreach (Pack p in packs)
            {
                s += p.ToString();
            }
            s += Environment.NewLine + "Items:" + Environment.NewLine;
            foreach (Item i in items)
            {
                s += i.ToString();
            }

            return s;
        }

        public int[] getadj()
        {
            return adj;
        }
    }
}
