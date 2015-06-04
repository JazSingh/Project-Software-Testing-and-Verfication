using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ST_Project
{
    public class Replayer
    {
        string[] log;
        int index = -1;

        private GameManager gm;
        private GameState st;
        private Dungeon d;
        private Player p;

        public Replayer(string filename)
        {
            Oracle.DETERM = true;
            log = File.ReadAllLines(filename);
            foreach (string l in log)
            {
                Debug.WriteLine(l);
            }
        }

        public void Init()
        {
            Debug.WriteLine("Play!");
            bool done = false;
            Debug.WriteLine("done: " + done + " HasNext() " + HasNext());
            while (HasNext() && !done)
            {
                string cur = GetNext();
                Debug.WriteLine("line: " + cur);
                switch (cur)
                {
                    case "DUNGEON": d = CreateDungeon(); Debug.WriteLine("Parse Dungeon");  break;
                    case "PLAYER": p = CreatePlayer(); Debug.WriteLine("Parse Player"); break;
                    case "ACTIONS": DoActions(); Debug.WriteLine("Seed State");  done = true; break;
                    default: continue;
                }
            }
            Debug.WriteLine("Init Finished");
        }

        private string GetNext()
        {
            index++;
            return log[index].Trim();
        }

        public bool HasNext()
        {
            return index + 1 < log.Length;
        }

        public void SeedState()
        {
            gm = new GameManager(true);
            st = new GameState(d, p, true);
            gm.replay = true;
            Debug.WriteLine(st.GetDungeon().ToString());
            Debug.WriteLine(st.GetPlayer().ToString());
            gm.SetState(st);
        }

        private Dungeon CreateDungeon()
        {
            string cur = GetNext();
            int dSize = int.Parse(cur.Split()[1]);
            Debug.WriteLine("dungeon size: " + dSize);
            cur = GetNext();
            int interval = int.Parse(cur.Split()[1]);
            Debug.WriteLine("interval: " + dSize);
            cur = GetNext();
            int diff = int.Parse(cur.Split()[1]);
            Debug.WriteLine("level: " + dSize);

            Node[] nodes = new Node[dSize];

            while (HasNext() && !string.IsNullOrEmpty(cur = GetNext()))
            {
                Node node = ParseNode(cur);
                nodes[node.ID] = node;
                if (node.ID % interval == 0) nodes[node.ID].SetCapacity(node.ID / interval);
                Debug.WriteLine("Parsed node: " + node.ID);
            }

            return new Dungeon(nodes, diff, dSize, interval);
        }

        private Node ParseNode(string line)
        {
            string[] l = line.Split();
            int id = int.Parse(l[1]);
            int numNeigh = l.Length - 2;
            int[] nbs = new int[numNeigh];
            for (int i = 2; i < l.Length; i++)
                nbs[i - 2] = int.Parse(l[i]);
            return new Node(id, nbs);
        }

        private Player CreatePlayer()
        {
            int hpmax = int.Parse(GetNext().Split()[1]);
            int hp = int.Parse(GetNext().Split()[1]);
            int dmg = int.Parse(GetNext().Split()[1]);
            int score = int.Parse(GetNext().Split()[1]);
            Item item = GetItem(GetNext());
            int numPots = int.Parse(GetNext().Split()[1]);
            int numTC = int.Parse(GetNext().Split()[1]);
            int numMS = int.Parse(GetNext().Split()[1]);
            List<Item> items = GetBagPack(numPots, numTC, numMS);
            Debug.WriteLine("Player: " + hpmax + " " + hp + " " + dmg + " " + score + " " + item);
            //public Player(int hpmax, int hp, int dmg, int scr, Item item, List<Item> items)
            return new Player(hpmax, hp, dmg, score, item, items);
        }

        private Item GetItem(string line)
        {
            string item = line.Split()[2];
            switch (item)
            {
                case "HealthPotion": return new Health_Potion();
                case "MagicScroll": return new Magic_Scroll();
                case "TimeCrystal": return new Time_Crystal();
                default: return null;
            }
        }

        private List<Item> GetBagPack(int hppots, int tcs, int mss)
        {
            List<Item> l = new List<Item>();
            for (int i = 0; i < hppots; i++)
                l.Add(new Health_Potion());
            for (int j = 0; j < tcs; j++)
                l.Add(new Time_Crystal());
            for (int k = 0; k < mss; k++)
                l.Add(new Magic_Scroll());
            return l;
        }

        private int GetItemVal(string item)
        {
            if (item == "MagicScroll") return 7;
            else if (item == "HealthPotion") return 1;
            else if (item == "TimeCrystal") return 4;
            return 10;
        }


        public void DoActions()
        {
            SeedState();
            Debug.WriteLine("State Seeded!");
        }

        public void Step()
        {
            string cur = GetNext();
            string[] parts = cur.Split();
            if (parts[0] == "Fighting")
            { gm.Fight(); Debug.WriteLine("FIGHT"); }
            if (parts[0] == "highscore")
            { gm.WriteHighscore(parts[1]); Debug.WriteLine("HIGHSCORE"); }
            if (parts[0] == "using" && parts[1] == "potion")
            { gm.UsePotion(); Debug.WriteLine("potion used"); }
            if (parts[0] == "using" && parts[1] == "crystal")
            { gm.UseCrystal(); Debug.WriteLine("crystal used"); }
            if (parts[0] == "using" && parts[1] == "scroll" && parts.Length == 2)
            {
                Oracle.DETERMF = true;
                gm.UseScroll();
                Debug.WriteLine("scroll without explosion used");
                Oracle.DETERMF = false;
            }
            if (parts[0] == "using" && parts[1] == "scroll" && parts.Length == 6)
            { gm.UseScroll(); Debug.WriteLine("scroll with explosion used"); }
            if (parts[0] == "Moving" && parts[1] == "to")
            { gm.PlayerMoved(int.Parse(parts[2])); Debug.WriteLine("Player moved"); }
            if (parts[0] == "spawned" && parts[1] == "pack")
            {
                gm.GetDungeon().nodes[int.Parse(parts[3])].pushPack(new Pack(GetItemVal(parts[5]))); Debug.WriteLine("Pack spawned"); 
            }
            if (parts[0] == "In" && parts[2] == "wordt" && parts[3] == "een" && parts[4] == "Item" && parts[5] == "gedropt:")
            { gm.GetDungeon().nodes[int.Parse(parts[1])].Add_Item(GetItem("Dropped Item: " + parts[6])); Debug.WriteLine("Item dropped"); }
        }


        public GameState QueryState()
        {
            return gm.GetState();
        }
    }
}
