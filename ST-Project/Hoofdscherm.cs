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
        GameManager parent;
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

        private void newgame_b_Click(object sender, EventArgs e)
        {
            diff.Visible = true;
            dif1.Visible = true;
            dif2.Visible = true;
            dif3.Visible = true;
            dif4.Visible = true;
            dif5.Visible = true;
        }

        OpenFileDialog ofd = new OpenFileDialog();


        private void button2_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                string[] filelines = File.ReadAllLines(filename);

                #region player

                int hpmax = Convert.ToInt32(filelines[1].Split(' ')[1]);
                int hp = Convert.ToInt32(filelines[2].Split(' ')[1]);
                int damage = Convert.ToInt32(filelines[3].Split(' ')[1]);
                int score = Convert.ToInt32(filelines[4].Split(' ')[1]);
                Item item;
                List<Item> items = new List<Item>();
                string type = filelines[6].Split(' ')[1];
                item = GenerateItem(type);
                int numitems = Convert.ToInt32(filelines[7].Split(' ')[1]);

                for (int i = 0; i < numitems; i++)
                {
                    string typ = filelines[8].Split(' ')[1];

                    items.Add(GenerateItem(typ));
                }

                Player p = new Player(hpmax, hp, damage, score, item, items);

                #endregion

                #region dungeon

                int index = 9;
                while (filelines[index] != "DUNGEON")
                {
                    index++;
                }

                int size = Convert.ToInt32(filelines[index + 1].Split(' ')[1]);
                int interval = Convert.ToInt32(filelines[index + 2].Split(' ')[1]);
                int difficulty = Convert.ToInt32(filelines[index + 3].Split(' ')[1]);
                index += 4;
                Node[] nodes = new Node[size];
                for (int i = 0; i < size; i++)
                {
                    string[] nodeline = filelines[index + i].Split(' ');


                    int identifier = Convert.ToInt32(nodeline[1]);
                    int[] adj = new int[nodeline.Length - 1];
                    for (int j = 1; j < nodeline.Length; j++)
                    {
                        adj[j - 1] = Convert.ToInt32(nodeline[j]);
                    }
                    Node n = new Node(identifier, adj);
                    nodes[i] = n;
                }


                Dungeon d = new Dungeon(nodes, difficulty, size, interval);

                #endregion

                GameState gs = new GameState(d, p);
            }
        }

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
    }
}