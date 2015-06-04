using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    public class Dungeon
    {
        GameState parent;
        public Node[] nodes;
        public int difficulty;
        public int dungeonSize;
        public int interval;
        public bool[] overcome;

        private int initialPackDrops;

        //Pre: n >= 0
        //Post difficulty, dungeonSize, interval initialized
        public Dungeon(int n)
        {
            difficulty = n;
            overcome = new bool[n];
            int k = 5; //Oracle.GiveNumber(4,6);
            dungeonSize = k * n + n + 2;
            interval = (int) Math.Ceiling((double) dungeonSize / (difficulty + 1));
            nodes = new Node[dungeonSize];
            initialPackDrops = 0;
            for (int i = 1; i <= n; i++)
                initialPackDrops += i;
            GenerateDungeon();
            //Console.WriteLine(ToString());
        }

        //Test Constructor
        public Dungeon(int n, bool mock)
        {
            difficulty = n;
            overcome = new bool[n];
            int k = 5; //Oracle.GiveNumber(4,6);
            dungeonSize = k * n + n + 2;
            interval = (int)Math.Ceiling((double)dungeonSize / (difficulty + 1));
            nodes = new Node[dungeonSize];
            initialPackDrops = 0;
            for (int i = 1; i <= n; i++)
                initialPackDrops += i;
        }

        // Load constructor
        public Dungeon(Node[] nds, int diff, int size, int interv)
        {
            overcome = new bool[diff];
            nodes = nds;
            difficulty = diff;
            dungeonSize = size;
            interval = interv;
        }

        //Pre: a non existing graph
        //Post: A connected grap in which all nodes are reachable
        public void GenerateDungeon()
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

            FixLooseEnds();
            //Connect paritions
            int rightPartition = difficulty;
            int leftPartition = difficulty-1;

            while (rightPartition > 0)
                ConnectParition(leftPartition--, rightPartition--);

            //add random edges in each parition;
            for (int i = 0; i <= difficulty; i++)
                AddRandomEdges(i);
        }

        //Pre: for all n in nodes: n = null;
        //Post: at least *difficulty* nodes are initialized
        public void CreateNodes()
        {
            int i = 1;
            int b = 1;
            while (i < dungeonSize)
            {
                if (i == b * interval)
                {
                    nodes[i] = new Node(i);
                    nodes[i].SetCapacity(b);
                    b++;
                }
                else if (Oracle.Decide())
                    nodes[i] = new Node(i);
                i++;
            }
        }

        //[S...b_1>, [b_1..b_2>, ...., [b_n-1...E-1], [E]
        //Create a spanning tree for the nodes in a partition
        //Pre: nodes[] contains at least 2 initialized nodes
        //Post: The nodes in a partition form a spanning tree
        public void CreateSpanningTree(int partition)
        {
            //Create list with indices of nodes in the partition
            List<int> partitionList = new List<int>();
            int lo = interval * partition;
            int hi = lo + interval;
            if (hi > dungeonSize) hi = dungeonSize;
            for(int i = lo; i < hi; i++)
                if(nodes[i] != null)
                    partitionList.Add(i);

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

        //Pre: nodes[] has some nodes and the nodes have some neighbours
        //Post: If there is a node which loosly hangs on the exit nodes,
        //         is is connected to some node in the k-1 th parition.
        public void FixLooseEnds()
        {
            //Find node from end that only has the exit node as neighbour
            //Connect with other node from that partition
            foreach(int n in nodes[dungeonSize-1].GetNeighbours())
                if (nodes[n].NumNeighbours == 1)
                {
                    for (int i = dungeonSize - 2; i >= difficulty * interval; i--)
                        if (i != n && nodes[i] != null
                            && !nodes[i].IsFull()
                            && nodes[n].NumNeighbours == 1)
                        {
                            nodes[n].AddNeighbour(i);
                            nodes[i].AddNeighbour(n);
                        }
                }
        }

        //Pre: p2-p1 = 1, p1 ^ p2 valid
        //Post: There is a path from a node u in p1 to a node v in p2
        public void ConnectParition(int p1, int p2)
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

        //Pre: nodes[n] != null, A fight is initiated
        //Post: A boolean indicitaing whether a pack has retreated from the battle.
        public bool CheckRetreat(int pos)
        {
            Node n = GetNode(pos);
            if (n.Retreat())
            {
                int[] adj = n.get_Adj();
                int next = adj[Oracle.GiveNumber(0, n.NumNeighbours-1)];
                Pack p = nodes[pos].popPack();

                nodes[next].pushPack(p);
                Console.WriteLine("Pack RETREATS naar node "+next);
                return true;
            }

            return false;
        }

        //Pre: nodes[player] != null
        //Post: Some packs might have moved through the dungeon
        public void MovePacks(int player, int LKP, int defend)
        {
            for (int t = 0; t < nodes.Length; t++)
            {
                Node n = GetNode(t);
                if (n != null && t != player)
                {
                    if (n.hasPack())
                    {
                        Stack<Pack> packs = n.getPacks(); // packs of node n
                        Stack<Pack> returns = new Stack<Pack>(); // stack where the packs which will contain the packs who won't move (this round)

                        for (int a = 0; a < packs.Count; a++)
                        {
                            Pack p = packs.Pop();
                            if (!p.is_Moved())
                            {
                                if (p.getHunt()) // if Pack is a Hunter
                                {
                                    bool moved = pack_Hunt(p, t, LKP);   // realize the Hunt-order
                                    if (!moved) // pack didn't move
                                    { returns.Push(p); Console.WriteLine("Hunter is niet verplaatst."); }
                                }

                                else if (p.getDefend()) // if Pack is a Defender
                                {
                                    bool moved = pack_Defend(p, t, defend);         // realize the Defend-order
                                    if (!moved) // pack didn't move
                                    { returns.Push(p); Console.WriteLine("Defender is niet verplaatst."); }
                                }

                                else // normal random movement
                                {
                                    int[] adj = n.GetNeighbours();
                                    int z = adj[Oracle.GiveNumber(0, adj.Length - 1)];
                                        if (z != nodes.Length - 1) // != end-node
                                        {
                                            Node zz = GetNode(z);
                                            int total = zz.TotalMonsters();
                                            if (total + p.GetNumMonsters() <= zz.maxCap())
                                            {
                                                p.Moved(true); // to prevent the system from moving the same pack again, later in this for-loop
                                                nodes[z].pushPack(p);
                                                //if(parent.parent.isLogging())
                                                //    parent.parent.unlogged.Enqueue("Pack moves van " + t + " naar node " + z + " HP: " + p.GetPackHealth());
                                                Console.WriteLine("Pack moves van " + t + " naar node " + z + " HP: " + p.GetPackHealth());
                                            }
                                            else // if the node the pack wants to move to, is full
                                                returns.Push(p);
                                        }
                                        else
                                            returns.Push(p);
                                }
                            }
                            else // if the pack moved already
                                returns.Push(p);
                        }
                        nodes[t].setPacks(returns);
                    }
                }
            }

            for (int t =0;t<nodes.Length;t++) // set for every pack isMoved to false
            {
                if (nodes[t] != null)
                {
                    Stack<Pack> packs = new Stack<Pack>();
                    while (nodes[t].hasPack())
                    {
                        Pack p = nodes[t].popPack();
                        p.Moved(false);
                        packs.Push(p);
                    }
                    while(packs.Count > 0)
                    {
                        nodes[t].pushPack(packs.Pop());
                    }

                }
            }
        }

        private bool pack_Hunt(Pack p, int pos, int LKP)
        {
            if (pos == LKP)
                return false;
            else
            {
                Stack<Node> path = ShortestPath(nodes[pos], nodes[LKP]);
                path.Pop();
                Node target = path.Pop(); // node the Pack needs to move to in this round
                int total = target.TotalMonsters();
                
                if (total + p.GetNumMonsters() <= target.maxCap())
                {
                    p.Moved(true); // to prevent the system from moving the same pack again, later in this for-loop
                    nodes[target.ID].pushPack(p);
                    Console.WriteLine("HUNTER moves van " + pos + " naar node " + target.ID + " HP: " + p.GetPackHealth());
                    return true;
                }
                return false;
            }
        }

        private bool pack_Defend(Pack p, int pos, int defend)
        {
            if (pos == defend)
                return false;
            else 
            {
                Stack<Node> path = ShortestPath(nodes[pos], nodes[defend]);
                path.Pop();
                Node target = path.Pop(); // node the Pack needs to move to in this round
                int total = target.TotalMonsters();

                if (total + p.GetNumMonsters() <= target.maxCap())
                {
                    p.Moved(true); // to prevent the system from moving the same pack again, later in this for-loop
                    nodes[target.ID].pushPack(p);
                    Console.WriteLine("DEFENDER moves van " + pos + " naar node " + target.ID + " HP: " + p.GetPackHealth());
                    return true;
                }
                return false;
            }
            
        }
        //Pre: partition * interval < dungeonSize
        //Post: Some edges might have been added in the partition
        public void AddRandomEdges(int partition)
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

        //Pre: u and v != null
        //Post: A stack containing the minimal number of nodes to reach from u to v
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

        //Pre: u != null
        //Post: nodes only has nodes which are reachable from the exit node
        public int Destroy(Node u)
        {
            int[] neighs = u.GetNeighbours();
            int[] backup = new int[neighs.Length];
            foreach (int n in neighs) Console.WriteLine(n);
            neighs.CopyTo(backup, 0);
            foreach (int n in neighs) Console.WriteLine(n);

            foreach (int neigh in neighs)
                nodes[neigh].RemoveNeighbour(u.ID);
            nodes[u.ID] = null;

            bool[] reachable = new bool[dungeonSize];
            ReachableNodes(nodes[dungeonSize - 1], ref reachable);

            for (int i = 0; i < dungeonSize; i++)
                if (!reachable[i])
                    nodes[i] = null;

            int newpos = -1;
            //newpos = first reachable neighbour
            foreach (int mn in backup)
            {
                if (reachable[mn]) newpos =  mn;
            }

            return newpos;
        }

        //Pre: length(visited) = dungeonSize, u != null
        //Post: Nodes who are reachable from u are indicated with a true value in visited
        public void ReachableNodes(Node u, ref bool[] visited)
        {
            visited[u.ID] = true;
            int[] nbs = u.GetNeighbours();
            if (nbs == null) return;
            foreach (int v in u.GetNeighbours())
                if (!visited[v]) ReachableNodes(nodes[v], ref visited);
        }

        //Pre: t is a valid itemtype
        //Post: An item of type t is dropped at a random node.
        public void DropItem(ItemType t)
        {
            Item k;
            switch(t)
            {
                case ItemType.HealthPotion: k = new Health_Potion(); break;
                case ItemType.TimeCrystal: k = new Time_Crystal(); break;
                default: k = new Magic_Scroll(); break;
            }

            List<int> dropNodes = new List<int>();
            for (int i = 1; i < dungeonSize - 2; i++)
                if (nodes[i] != null)
                    dropNodes.Add(i);

            int selected = dropNodes[Oracle.GiveNumber(dropNodes.Count-1)];

            Console.WriteLine("In " + selected + " wordt een Item gedropt.");
            if(parent != null && parent.parent.isLogging() && k.ToString() != "MagicScroll")
                parent.parent.unlogged.Enqueue("In " + selected + " wordt een Item gedropt: " + k.ToString());
            nodes[selected].Add_Item(k);
        }

        //Pre: nodes has some initialized nodes.
        //Post: the number of packs spawned = sum (i = 0 to diff) i = n(n+1) / 2
        public void SpawnMonsters()
        {
            //spawn 1 on every bridge
            int dropped = 0;
            for (int i = 1; i <= difficulty; i++)
            {
                nodes[i * interval].AddPack(AddPotionCheck());
                if (parent != null && parent.parent.isLogging())
                {
                    Item itm = nodes[i * interval].getPacks().Peek().GetItem();
                    if (itm != null)
                        parent.parent.unlogged.Enqueue("spawned pack on " + i * interval + " met " + itm.ToString());
                    else
                        parent.parent.unlogged.Enqueue("spawned pack on " + i * interval);
                }
            }
            dropped = difficulty;
                //drop on random nodes
            for (int i = 1; i < dungeonSize - 2; i++)
                if (initialPackDrops - dropped > 0
                    && nodes[i] != null
                    && Oracle.Decide() && nodes[i].AddPack(AddPotionCheck()))
                {
                    dropped++;
                    if (parent != null && parent.parent.isLogging())
                        parent.parent.unlogged.Enqueue("spawned pack on " + i);
                }
            //fill bridges with amount that is left
            int j = difficulty;
            while(initialPackDrops - dropped > 0)
            {
                if (nodes[j * interval].AddPack(AddPotionCheck()))
                {
                    dropped++;
                    if (parent != null && parent.parent.isLogging())
                        parent.parent.unlogged.Enqueue("spawned pack on " + j*interval);
                }
                j = j == 1 ? difficulty : j - 1;
            }
        }

        private bool AddPotionCheck()
        {
            try{
                return parent.GetPlayer().GetHP() + num_potions() * 25 <= hpMonsters();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return false;
        }

        private int hpMonsters()
        {
            int total = 0;

            for (int t =0;t<nodes.Length;t++)
            {
                if (nodes[t] != null)
                {
                    Stack<Pack> packs = nodes[t].getPacks();
                    foreach(Pack p in packs)
                    {
                        total += p.GetPackHealth();
                    }
                }
            }
            return total;
        }

        private int num_potions()
        {
            int total = 0;

            for (int t = 0; t < nodes.Length; t++)
            {
                if (nodes[t] != null)
                {
                    List<Item> items = nodes[t].get_Items();
                    try
                    {
                        foreach (Item i in items)
                        {
                            if (i.type == ItemType.HealthPotion)
                                total++;
                        }
                    }
                    catch (Exception e) { }

                    try
                    {
                        Stack<Pack> packs = nodes[t].getPacks();
                        foreach (Pack p in packs)
                        {
                            if (p.GetItem().type == ItemType.HealthPotion)
                                total++;
                        }
                    }
                    catch (Exception e) { }
                }
            }

            List<Item> iis = parent.GetPlayer().getItems();
            try
            {
                foreach (Item i in iis)
                {
                    if (i.type == ItemType.HealthPotion)
                        total++;
                }
            }
            catch (Exception e) { }
            return total;
        }

        public Node GetNode(int i)
        {
            return nodes[i];
        }

        //Pre: - 
        //Post: Total sum of health of monsters in the dungeon
        public int SumMonsterHealth()
        {
            int sum = 0;
            for (int i = 0; i < dungeonSize; i++)
                if (nodes[i] != null)
                    sum += nodes[i].SumMonsterHealth();
            return sum;
        }

        //Pre:-
        //Post: Sum of health potions lying on the map give
        public int SumHealPots()
        {
            int sum = 0;
            for (int i = 0; i < dungeonSize; i++)
                if (nodes[i] != null)
                    sum += nodes[i].SumHealPots();
            return sum;
        }

        public override string ToString()
        {
            string s = string.Empty;
            s += "DUNGEON" + Environment.NewLine;
            s += "DungeonSize: " + dungeonSize + Environment.NewLine;
            s += "Interval: " + interval + Environment.NewLine;
            s += "Difficulty: " + difficulty + Environment.NewLine;

            for (int i = 0; i < dungeonSize; i++)
                if(nodes[i] != null)
                 s += nodes[i].ToString() + Environment.NewLine;

            return s;
        }

        public int getNumPacks()
        {
            int res = 0;
            for (int t =0;t<nodes.Length;t++)
            {
                if (nodes[t] != null)
                {
                    Stack<Pack> ps = nodes[t].getPacks();
                    res += ps.Count();
                }
            }

            return res;
        }

        public void GiveHuntOrder()
        {
            int numOrders = 3;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    var packs = nodes[i].getPacks();
                    Stack<Pack> newpacks = new Stack<Pack>();
                    while (packs.Count > 0 && numOrders > 0)
                    {
                        Pack p = packs.Pop();
                        if (!p.getHunt() && !p.getDefend())
                        {
                            if (numOrders == 3)
                                p.SetHunt();
                            else if (numOrders == 2 && Oracle.Decide())
                                p.SetHunt();
                            else if (numOrders == 1 && Oracle.Decide() && Oracle.Decide())
                                p.SetHunt();
                            numOrders--;
                        }
                        newpacks.Push(p);
                    }
                    foreach (Pack p in newpacks)
                        nodes[i].pushPack(p);
                }
            }
        }

        public void GiveDefendOrder(int bridgeLevel)
        {
            Console.WriteLine("DefendOrder received!!");
            if (bridgeLevel > difficulty) return;
            foreach (Pack p in nodes[bridgeLevel * interval].getPacks())
                p.SetDefend();
            int resCap = nodes[bridgeLevel * interval].GetCapacity() - nodes[bridgeLevel * interval].TotalMonsters();
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null && i != bridgeLevel * interval)
                {
                    var packs = nodes[i].getPacks();
                    Stack<Pack> newpacks = new Stack<Pack>();
                    while (packs.Count > 0 && resCap > 0)
                    {
                        Pack p = packs.Pop();
                        if (!p.getHunt() && !p.getDefend() && resCap - p.GetNumMonsters() >= 0)
                        {
                            p.SetDefend();
                            resCap -= p.GetNumMonsters();
                        }
                        newpacks.Push(p);
                    }
                    foreach (Pack p in newpacks)
                        nodes[i].pushPack(p);
                }
            }
        }

        public void iAmYourFather(GameState gs)
        {
            parent = gs;
        }
    }
}
