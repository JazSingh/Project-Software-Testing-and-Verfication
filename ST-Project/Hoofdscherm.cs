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
            //Dungeon d = new Dungeon(difficulty);
            //Gamescherm g = new Gamescherm(difficulty);
            //g.Show();
            parent.DiffSelectNotify(difficulty);
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
                // NEEDS IMPLEMENTATION HERE!
            }
        }
    }
}
