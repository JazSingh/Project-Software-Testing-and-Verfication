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

        private int MaxCapacity = 9;

        public Node(int i)
        {
            identifier = i;
            adj = new int[4];
            numNeighbours = 0;
            packs = new Stack<Pack>();
            items = new List<Item>();
        }

        public Node(int ident, int[] adje)
        {
            identifier = ident;
            adj = adje;
            numNeighbours = adj.Length;
            packs = new Stack<Pack>();
            items = new List<Item>();
        }

        public int[] get_Adj()
        {
            return adj;
        }

        public void SetCapacity(int bridgeLvl)
        {
            MaxCapacity *= bridgeLvl;
        }

        public int GetCapacity()
        {
            return MaxCapacity;
        }

        public void Add_Item(Item i)
        {
            items.Add(i);
        }

        public List<Item> get_Items()
        {
            return items;
        }

        public bool Retreat()
        {
            Pack p = packs.Pop();
            if (p.retreat())
            {
                Console.WriteLine("Pack retreats!");
                packs.Push(p);
                return true;
            }
            else
                packs.Push(p);
            return false;
        }

        public bool AddPack()
        {
            Pack p = new Pack();
            if (TotalMonsters() + p.GetNumMonsters() > MaxCapacity) return false;
            packs.Push(p);
            return true;
        }

        public Pack popPack()
        {
            return packs.Pop();
        }

        public void pushPack(Pack p)
        {
            packs.Push(p);
        }

        public int TotalMonsters()
        {
            int s = 0;
            foreach (Pack p in packs)
                s += p.GetNumMonsters();
            return s;
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

        public int SumMonsterHealth()
        {
            int sum = 0;
            foreach (Pack p in packs)
                sum += p.GetPackHealth();
            return sum;
        }

        public int SumHealPots()
        {
            int sum = 0;
            foreach (Item i in items)
            {
                if (i.type == ItemType.HealthPotion)
                    sum += i.health;
            }
            return sum;
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
            string s = "Node " + identifier.ToString() + " ";
            for (int i = 0; i < numNeighbours; i++)
                s += adj[i] + " ";

            /*
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
            */
             
            return s;
        }

        public int[] getadj()
        {
            return adj;
        }

        public bool hasPack()
        {
            if (packs.Count > 0)
                return true;
            return false;
        }

        public bool IsBridge(int interval)
        {
            if(identifier % interval == 0)
                return true;

            return false;
        }

    }
}
