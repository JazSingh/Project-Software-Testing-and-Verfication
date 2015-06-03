using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ST_Project
{
    public class Log
    {
        string[] args;
        int currline;
        GameManager parent;

        public Log(string[] args, GameManager gmr)
        {
            parent = gmr;
            currline = 0;
            this.args = args;
            init();
        }

        void replay()
        {

        }

        public void init()
        {
            Dungeon d = genDungeon();
            Player p = genPlayer();
            GameState gmst = new GameState(d, p);
            
        }

        Dungeon genDungeon()
        {
            int size = Convert.ToInt32(args[1].Split(' ')[1]);
            int interv = Convert.ToInt32(args[2].Split(' ')[1]);
            int diff = Convert.ToInt32(args[3].Split(' ')[1]);

            currline = 4;
            string currstr = args[currline];

            Node[] nodes = new Node[size];


            while (currstr != "")
            {
                string[] nodeline = args[currline].Split(' ');

                int identifier = Convert.ToInt32(nodeline[1]);
                int[] adj = new int[nodeline.Length - 3];

                for (int j = 2; j < nodeline.Length - 1; j++)
                {
                    adj[j - 2] = Convert.ToInt32(nodeline[j]);
                }

                Node n = new Node(identifier, adj);
                nodes[identifier] = n;

                currline++;
                currstr = args[currline];
            }
            currline += 1;

            Dungeon d = new Dungeon(nodes, diff, size, interv);
            return d;
        }

        Player genPlayer()
        {
            int hpmax = Convert.ToInt32(args[currline+1].Split(' ')[1]);
            int hp = Convert.ToInt32(args[currline+2].Split(' ')[1]);
            int damage = Convert.ToInt32(args[currline+3].Split(' ')[1]);
            int score = Convert.ToInt32(args[currline+4].Split(' ')[1]);

            Item item;
            List<Item> items = new List<Item>();
            string type = args[currline+5].Split(' ')[2];
            switch (type)
            {
                case "HealthPotion": item = new Health_Potion(); break;
                case "TimeCrystal": item = new Time_Crystal(); break;
                case "MagicScroll": item = new Magic_Scroll(); break;
                default: item = null; break;
            }

            int hpcount = Convert.ToInt32(args[currline+6].Split(' ')[1]);
            int tccount = Convert.ToInt32(args[currline+7].Split(' ')[1]);
            int mscount = Convert.ToInt32(args[currline+8].Split(' ')[1]);

            for (int i = 0; i < hpcount; i++) items.Add(new Health_Potion());
            for (int i = 0; i < tccount; i++) items.Add(new Time_Crystal());
            for (int i = 0; i < mscount; i++) items.Add(new Magic_Scroll());

            Player p = new Player(hpmax, hp, damage, score, item, items);
            currline += 10;

            return p;
        }

        public bool hasNext()
        {
            return false;
        }

        public void next()
        {

        }
        public string getStep()
        {
            return string.Empty;
        }
    }
}
