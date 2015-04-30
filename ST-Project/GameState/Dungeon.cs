using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project.GameState
{
    class Dungeon
    {
        private Node[] nodes;
        private bool[] bridges;
        private int difficulty;
        private int dungeonSize;
        private int interval;

        public Dungeon(int n)
        {
            difficulty = n;
            int k = Orcale.GiveNumver(3,5);
            int dungeonSize = k * n + n + 2;
            int interval = dungeonSize / (difficulty + 1);
            nodes = new Node[dungeonSize];
        }

        private void GenerateDungeon()
        {
            //Initialize start and exit node
            nodes[0] = new Node(0);
            nodes[dungeonSize-1] = new Node(dungeonSize-1);
            //Determine position of all bridges to create partitions
            //Determine nodes in each partition
            //Create Spanning Tree
            //Add edges
        }

        private void CreateNodes()
        {
            int i = 1;
            int b = 1;
            while (i < dungeonSize)
            {
                if (i == b * interval)
                {
                    nodes[i] = new Node(i);
                    b++;
                }
                else if (Orcale.Decide())
                    nodes[i] = new Node(i);
                i++;
            }
        }

        //[S...b_1>, [b_1..b_2>, ...., [b_n-1...E-1], [E]
        //Create a spanning tree for the nodes in a partition
        private void CreateSpanningTree(int partition)
        {
            //Create list with indices of nodes in the partition
            List<int> partitionList = new List<int>();
            int lo = interval * partition;
            int hi = lo + interval;
            for(int i = lo; i < hi; i++)
                if(nodes[i] != null)
                    partitionList.Add(i);

            int u;
            int v;
            u = partitionList[Orcale.GetNumber(partitionList.Count-1)];
            partitionList.Remove(u);
            while(partitionList.Count != 0)
            {
                v = partitionList[Orcale.GetNumber(partitionList.Count-1)];
                partitionList.Remove(v);
                nodes[u].AddNeighbour(nodes[v].ID);
                nodes[v].AddNeighbour(nodes[u].ID);
                u = v;
            }     
        }

        //Pre: p2-p1 = 1
        //Post: There is a path from a node u in p1 to a node v in p2
        private void ConnectParition(int p1, int p2)
        {

        }

        //BFS
        public int ShortestPath(Node u, Node v)
        {
            return 0;
        }

        
        public int Destroy(Node u)
        {
            return 0;
        }

        public override string ToString()
        {
            string s = string.Empty;
            foreach(Node n in nodes)
                s += n.ToString() + Environment.NewLine;
            return s;
        }
    }
}
