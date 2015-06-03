using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ST_Project
{
    class Replayer
    {
        string[] log;
        int index = -1;

        public GameManager gm;
        public GameState st;
        public Dungeon d;
        public Player p;

        public Replayer(string filename)
        {
            Oracle.DETERM = true;
            log = File.ReadAllLines(filename);
        }

        public void Play()
        {
            while (HasNext())
            {
                string cur = GetNext();
                switch(cur)
                {
                    case "DUNGEON": d = CreateDungeon(); break;
                    case "PLAYER": p = CreatePlayer(); break;
                    case "ACTIONS": DoActions(); break;
                    default: continue;
                }
            }
        }

        public string GetNext()
        {
            index++;
            return log[index].Trim();
        }

        public bool HasNext()
        {
            return index + 1 < log.Length - 1;
        }

        public void SeedState()
        {
            gm = new GameManager(true);
            st = new GameState(d, p, true);
            gm.state = st;
        }

        private Dungeon CreateDungeon()
        {
            string cur = GetNext();
            int dSize = int.Parse(cur.Split()[1]);
            cur = GetNext();
            int interval = int.Parse(cur.Split()[1]);
            cur = GetNext();
            int diff = int.Parse(cur.Split()[1]);

            Node[] nodes = new Node[dSize];

            while (HasNext() && !string.IsNullOrEmpty(cur = GetNext()))
            {
                Node node = ParseNode(cur);
                nodes[node.ID] = node;
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
            if (item == "HealthPotion") return 1;
            return 4;
        }


        public void DoActions()
        {
            SeedState();
            string cur;
            while((cur = GetNext()) != "End node")
            {
                string[] parts = cur.Split();
                if(parts[0] == "Fighting")
                    gm.Fight();
                if (parts[0] == "using" && parts[1] == "potion")
                    gm.UsePotion();
                if (parts[0] == "using" && parts[1] == "crystal")
                    gm.UseCrystal();
                if(parts[0] == "using" && parts[1] == "scroll" && parts.Length == 2)
                {
                    Oracle.DETERMF = true;
                    gm.UseScroll();
                    Oracle.DETERMF = false;
                }
                if(parts[0] == "using" && parts[1] == "scroll" && parts.Length == 6)
                    gm.UseScroll();

                if(parts[0] == "Moving" && parts[1] == "to")
                    gm.PlayerMoved(int.Parse(parts[2]));
                if (parts[0] == "spawned" && parts[1] == "pack")
                    gm.GetDungeon().nodes[int.Parse(parts[3])].pushPack(new Pack(GetItemVal(parts[5])));
                if (parts[0] == "In" && parts[2] == "wordt" && parts[3] == "een" && parts[4] == "Item" && parts[5] == "gedropt:")
                    gm.GetDungeon().nodes[int.Parse(parts[3])].Add_Item(GetItem("Item: " + parts[6]));

            }
        }
    }
}
