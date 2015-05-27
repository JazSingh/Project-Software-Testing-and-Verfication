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

        //Post: member variables are initialized
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
            adj = new int[4];
            numNeighbours = 0;
            for (int i = 0; i < adje.Length; i++)
            {
                numNeighbours++;
                adj[i] = adje[i];
            }
            packs = new Stack<Pack>();
            items = new List<Item>();
        }

        public int[] get_Adj()
        {
            return adj;
        }

        //Set maxcapacity if the node is a bridge
        public void SetCapacity(int bridgeLvl)
        {
            MaxCapacity *= bridgeLvl;
        }

        public int GetCapacity()
        {
            return MaxCapacity;
        }

        //Add item to node
        public void Add_Item(Item i)
        {
            items.Add(i);
        }

        public List<Item> get_Items()
        {
            return items;
        }

        public void RemoveItems()
        {
            items.Clear();
        }

        //Check if upper most pack in the stack retreats
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

        //Add a new pack to the node
        public bool AddPack()
        {
            Random r = new Random();
            int val = r.Next(0, 19);
            Pack p = new Pack(val);
            if (TotalMonsters() + p.GetNumMonsters() > MaxCapacity) return false;
            packs.Push(p);
            return true;
        }

        public Stack<Pack> getPacks()
        {
            return packs;
        }

        //Remove pack from stack
        public Pack popPack()
        {
            return packs.Pop();
        }

        //Add pack to stack
        public void pushPack(Pack p)
        {
            packs.Push(p); // else set new pack on fst place
        }

        //Get total number of monsters in node
        public int TotalMonsters()
        {
            int s = 0;
            foreach (Pack p in packs)
                s += p.GetNumMonsters();
            return s;
        }

        //Add neighbour if possible and not already present
        public void AddNeighbour(int node)
        {
            if(!IsNeighbour(node) && numNeighbours < 4)
                adj[numNeighbours++] = node; 
        }

        //Check if some node is a neighbour
        public bool IsNeighbour(int node)
        {
            for(int i = 0; i < numNeighbours; i++)
                if(adj[i] == node) return true;
            return false;
        }

        //Return an array with the indices of the neigher or null if there are no neighbours
        public int[] GetNeighbours()
        {
            if (numNeighbours == 0) return null;
            int[] neighs = new int[numNeighbours];
            for (int i = 0; i < numNeighbours; i++)
                neighs[i] = adj[i];
            return neighs;
        }

        public bool IsFull()
        {
            return numNeighbours == 4;
        }

        //If v is a neighbour, remove it.
        public bool RemoveNeighbour(int v)
        {
            int index = -1;
            for (int i = 0; i < numNeighbours; i++)
                if (adj[i] == v) index = i;
            if (index == -1) return false;

            adj[index] = 0;
            for (int i = index; i < numNeighbours - 1; i++)
                adj[i] = adj[i + 1];
            numNeighbours--;
            return true;
        }

        //Return the sum of health of monster in the node
        public int SumMonsterHealth()
        {
            int sum = 0;
            foreach (Pack p in packs)
                sum += p.GetPackHealth();
            return sum;
        }

        //Return the sum of health the potions inside the node
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
            return s;
        }

        //Check if the node contains a pack
        public bool hasPack()
        {
            if (packs.Count > 0)
                return true;
            return false;
        }

        //Given the interval of a dungeon, check if the node is bridge.
        public bool IsBridge(int interval)
        {
            if(identifier > 0 && identifier % interval == 0)
                return true;

            return false;
        }

        public int maxCap()
        {
            return MaxCapacity;
        }
    }
}
