using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

                //PLAYER
                int hpmax = Convert.ToInt32(filelines[1].Split(' ')[1]);
                int hp = Convert.ToInt32(filelines[2].Split(' ')[1]);
                int damage = Convert.ToInt32(filelines[3].Split(' ')[1]);
                int score = Convert.ToInt32(filelines[4].Split(' ')[1]);
                Item i;
                List<Item> items = new List<Item>();
                string type = filelines[6].Split(' ')[1];
                if (type == "none")
                    i = null;
                else
                {
                    switch (type)
                    {
                        case "HealthPotion": i = new Health_Potion(); break;
                        case "TimeCrystal": i = new Time_Crystal(); break;
                        case "MagicScroll": i = new Magic_Scroll(); break;
                        default: i = null; break;
                    }
                    // TODO invullen Item waardes geven

                }

                Player p = new Player(hpmax, hp, damage, score, i, items);

                //DUNGEON

                //TODO dungeon uitlezen
                Dungeon d = null;

                //New GameState

                GameState gs = new GameState(d, p);
            }
        }
    }
}
