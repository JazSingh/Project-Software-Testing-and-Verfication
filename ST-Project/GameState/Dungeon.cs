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
                    nodes[i] = new Node(i);
                else if (Orcale.Decide())
                    nodes[i] = new Node(i);
                i++;
            }
        }

        //[S...b_1>, [b_1..b_2>, ...., [b_n-1...E-1], [E]
        //Create a spanning tree for the nodes in a partition
        private void CreateSpanningTree(int partition)
        {
            List<int> partitionList = new List();
            int lo = interval * partition;
            int hi = lo + interval;
            for(int i = lo; i < hi; i++)
                if(nodes[i] != null)
                    partitionList.Add(i);
            bool[] inTree = new bool[partitionList.Length];
            
            //TODO
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
    }
}
