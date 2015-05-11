using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST_Project
{
    public partial class Hoofdscherm : Form
    {
        GameManager parent;     // GameManager parent object to communicate with
        public Hoofdscherm(GameManager parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Height = 720;
            this.Width = 1280;
        }

        private void DungeonRPG_Load(object sender, EventArgs e)
        {

        }

        private void dif_Click(object sender, EventArgs e)
        {
            string[] source = sender.ToString().Split(' ');
            int difficulty = int.Parse(source[2]);
            parent.DiffSelectNotify(difficulty);
            Visible = false;
        }

        // create a new game
        private void newgame_b_Click(object sender, EventArgs e)
        {
            parent.DiffSelectNotify(1);
        }

        OpenFileDialog ofd = new OpenFileDialog();

        // loads a game from a save file
        private void button2_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                string[] filelines = File.ReadAllLines(filename);

                // this region deals with reading all lines needed to create a player object
                #region player
                
                int hpmax = Convert.ToInt32(filelines[1].Split(' ')[1]);
                int hp = Convert.ToInt32(filelines[2].Split(' ')[1]);
                int damage = Convert.ToInt32(filelines[3].Split(' ')[1]);
                int score = Convert.ToInt32(filelines[4].Split(' ')[1]);

                Item item;
                List<Item> items = new List<Item>();
                string type = filelines[5].Split(' ')[2];
                item = GenerateItem(type);

                int hpcount = Convert.ToInt32(filelines[6].Split(' ')[1]);
                int tccount = Convert.ToInt32(filelines[7].Split(' ')[1]);
                int mscount = Convert.ToInt32(filelines[8].Split(' ')[1]);

                for (int i = 0; i < hpcount; i++) items.Add(new Health_Potion());
                for (int i = 0; i < tccount; i++) items.Add(new Time_Crystal());
                for (int i = 0; i < mscount; i++) items.Add(new Magic_Scroll());

                Player p = new Player(hpmax, hp, damage, score, item, items);

                #endregion

                // this region deals with reading all lines needed to create a dungeon object
                #region dungeon

                int size = Convert.ToInt32(filelines[11].Split(' ')[1]);
                int interval = Convert.ToInt32(filelines[12].Split(' ')[1]);
                int difficulty = Convert.ToInt32(filelines[13].Split(' ')[1]);

                Node[] nodes = new Node[size];

                for (int i = 14; i < filelines.Length; i++)
                {
                    string[] nodeline = filelines[i].Split(' ');

                    int identifier = Convert.ToInt32(nodeline[1]);
                    int[] adj = new int[nodeline.Length - 3];

                    for (int j = 2; j < nodeline.Length-1; j++)
                    {
                        adj[j - 2] = Convert.ToInt32(nodeline[j]);
                    }

                    Node n = new Node(identifier, adj);
                    nodes[identifier] = n;
                }


                Dungeon d = new Dungeon(nodes, difficulty, size, interval);

                #endregion

                // the Player p and Dungeon d together make a new GameState gs
                GameState gs = new GameState(d, p);

                parent.GameLoadNotify(gs, difficulty);
            }
        }

        // returns the right item for the save file Load method
        private Item GenerateItem(string s)
        {
            Item it;

            switch (s)
            {
                case "HealthPotion": it = new Health_Potion(); break;
                case "TimeCrystal": it = new Time_Crystal(); break;
                case "MagicScroll": it = new Magic_Scroll(); break;
                default: it = null; break;
            }

            return it;
        }

        // shows the highscores
        private void button1_Click(object sender, EventArgs e)
        {
            parent.ShowHighScores();
        }
    }
}
