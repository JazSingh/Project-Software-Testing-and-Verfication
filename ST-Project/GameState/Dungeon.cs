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
        private int difficulty;
        private int dungeonSize;
        private int interval;

        public Dungeon(int n)
        {
            difficulty = n;
            int k = Oracle.GiveNumber(4,6);
            dungeonSize = k * n + n + 2;
            interval = (int) Math.Ceiling((double) dungeonSize / (difficulty + 1));
            nodes = new Node[dungeonSize];
            GenerateDungeon();
            Console.WriteLine(ToString());
        }

        private void GenerateDungeon()
        {
            //Initialize start and exit node
            nodes[0] = new Node(0);
            nodes[dungeonSize-1] = new Node(dungeonSize-1);
            //Determine position of all bridges to create partitions
            //Determine nodes in each partition
            CreateNodes();
            //Create Spanning Tree
            for (int i = 0; i < difficulty + 1; i++)
                CreateSpanningTree(i);

            //Connect paritions
            int rightPartition = difficulty;
            int leftPartition = difficulty-1;

            while (rightPartition > 0)
                ConnectParition(leftPartition--, rightPartition--);

            //add random edges in each parition;
            for (int i = 0; i <= difficulty; i++)
                AddRandomEdges(i);
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
                else if (Oracle.Decide())
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
            if (hi > dungeonSize) hi = dungeonSize;
            for(int i = lo; i < hi; i++)
                if(nodes[i] != null)
                    partitionList.Add(i);

            if (partitionList.Count == 0) return;
            int u;
            int v;
            u = partitionList[Oracle.GiveNumber(partitionList.Count-1)];
            partitionList.Remove(u);
            while(partitionList.Count != 0)
            {
                v = partitionList[Oracle.GiveNumber(partitionList.Count-1)];
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
            int min_p1 = p1 * interval;
            int max_p1 = min_p1 + interval - 1;

            int v = p2 * interval; //bridge node index of p2
            
            List<int> nodeList = new List<int>();
            for (int i = min_p1; i <= max_p1; i++)
                if (nodes[i] != null)
                    nodeList.Add(i);

            int u = nodeList[Oracle.GiveNumber(nodeList.Count-1)];

            nodes[u].AddNeighbour(nodes[v].ID);
            nodes[v].AddNeighbour(nodes[u].ID);
        }

        private void AddRandomEdges(int partition)
        {
            int min = partition * interval;
            int max = min + interval;
            if(max > dungeonSize - 1) max = dungeonSize - 1;

            for (int i = min; i <= max; i++)
            {
                for (int j = i; j <= max; j++)
                {
                    if (i != j && nodes[i] != null && nodes[j] != null 
                        && !nodes[i].IsNeighbour(j) 
                        && !nodes[i].IsFull() && !nodes[j].IsFull()
                        && Oracle.Decide() && Oracle.Decide())
                    {
                        nodes[i].AddNeighbour(j);
                        nodes[j].AddNeighbour(i);
                    }
                }
            }
        }

        //BFS
        public Stack<Node> ShortestPath(Node u, Node v)
        {
            Stack<Node> path = new Stack<Node>();

            Queue<int> queue = new Queue<int>();
            bool[] visited = new bool[nodes.Length];
            int[] prev = new int[nodes.Length];


            int node = u.ID;
            prev[node] = -1;
            queue.Enqueue(node);

            while (queue.Count > 0 && node != v.ID)
            {
                node = queue.Dequeue();
                visited[node] = true;
                
                foreach(int i in nodes[node].GetNeighbours())
                {
                    if (!visited[i])
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                        prev[i] = node;
                    } 
                }
            }

            int previous = node;

            while (previous != -1)
            {
                path.Push(nodes[previous]);
                previous = prev[previous];
            }

            return path;
        }


        
        public void Destroy(Node u)
        {
            int[] neighs = u.GetNeighbours();
            foreach (int neigh in neighs)
                nodes[neigh].RemoveNeighbour(u.ID);
            nodes[u.ID] = null;

            bool[] reachable = new bool[dungeonSize];
            ReachableNodes(nodes[dungeonSize - 1], ref reachable);

            for (int i = 0; i < dungeonSize; i++)
                if (nodes[i] != null && !reachable[i])
                    nodes[i] = null;
        }

        private void ReachableNodes(Node u, ref bool[] visited)
        {
            visited[u.ID] = true;
            foreach (int v in u.GetNeighbours())
                if (!visited[v]) ReachableNodes(nodes[v], ref visited);
        }

        public Node GetNode(int i)
        {
            return nodes[i];
        }

        public override string ToString()
        {
            string s = string.Empty;
            s += "DungeonSize: " + dungeonSize + Environment.NewLine;
            s += "Interval: " + interval + Environment.NewLine;

            for (int i = 0; i < dungeonSize; i++)
                if(nodes[i] != null)
                 s += nodes[i].ToString() + Environment.NewLine;

            return s;
        }
    }
}
