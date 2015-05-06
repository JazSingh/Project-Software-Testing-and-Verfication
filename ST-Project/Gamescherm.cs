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
    public partial class Gamescherm : Form
    {
        GameManager parent;
        Dictionary<int, Tuple<int, int>> locations;
        int w = 30;
        int h = 30;

        public Gamescherm(int i, GameManager gs)
        {
            InitializeComponent();
            this.parent = gs;
            Paint += teken;
            locations = new Dictionary<int, Tuple<int, int>>();
        }

        public void teken(object sender, PaintEventArgs e)
        {
            drawDungeon(e);
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            NRpotions.Text = parent.GetPlayer().getNRPotions().ToString();
            NRcrystals.Text = parent.GetPlayer().getNRCrystals().ToString();
            NRscrolls.Text = parent.GetPlayer().getNRScrolls().ToString();
            NRhealth.Text = parent.GetPlayer().getHealth().ToString();
        }

        private void drawDungeon(PaintEventArgs e)
        {
            if (locations.Count == 0)
            { setupDungeon(e); }

            Graphics gr = e.Graphics;
            int h = 30;
            int w = 30;
            Font drawFont = new Font("Arial", 16);

            // look for current position of player
            int pos = parent.GetPlayer().get_position();
            int[] adjs = parent.GetState().GetDungeon().GetNode(pos).get_Adj();

            Tuple<int, int> k = locations[pos];

            int x_pos = k.Item1 + (int)(0.5 * w);
            int y_pos = k.Item2 + (int)(0.5 * h);

            // draw Edges
            for (int t = 0; t < parent.GetDungeon().GetNode(pos).NumNeighbours; t++)
            {
                int buur = adjs[t];

                int x_end = locations[buur].Item1 + (int)(0.5 * w);
                int y_end = locations[buur].Item2 + (int)(0.5 * h);
                gr.DrawLine(Pens.Black, x_pos, y_pos, x_end, y_end);

                // draw Neighbours

                Brush color = Brushes.Black;
                if (buur != 0 && buur % parent.GetDungeon().interval == 0)
                    color = Brushes.Orange;             // color of Bridge
                else if (buur == parent.GetDungeon().nodes.Length - 1)
                    color = Brushes.Red;                // color of end-node

                gr.FillEllipse(color, locations[buur].Item1, locations[buur].Item2, w, h);
            }

            // draw Current Node
            gr.FillEllipse(Brushes.Green, locations[pos].Item1, locations[pos].Item2, w, h);
        }

        private bool isNeighbour(int[] adjs, int p)
        {
            for (int t = 0; t < adjs.Length; t++)
                if (adjs[t] == p)
                    return true;
            return false;
        }

        private void setupDungeon(PaintEventArgs e)
        {

            Graphics gr = e.Graphics;

            int x = 100;
            int y = 200;
            int x_dist = 0;
            switch (parent.GetDungeon().difficulty)
            {
                case 5: { x_dist = 420 - (60 * (parent.GetDungeon().difficulty - 1)); break; }
                default: { x_dist = 420 - (60 * (parent.GetDungeon().difficulty)); break; }
            }
            int y_dist = 80;

            Node u = parent.GetDungeon().nodes[0];
            Node v = parent.GetDungeon().nodes[parent.GetDungeon().nodes.Length - 1];
            Stack<Node> pad = parent.GetDungeon().ShortestPath(u, v);

            Font drawFont = new Font("Arial", 16);

            ///////////// draw Dungeon, based on partitions

            // calc node-positions 
            // WARNING: 
            // DON'T LOOK AT THIS CODE, IT'S PURE MAGIC
            // ONLY THE WRITER KNOWS WHAT IT MEANS...
            for (int t = 0; t < parent.GetDungeon().nodes.Length; t += parent.GetDungeon().interval)
            {
                if (parent.GetDungeon().nodes[t] != null)
                {
                    if (t % parent.GetDungeon().interval == 0)
                    {
                        locations.Add(t, new Tuple<int, int>(x + (int)(0.5 * w), y + (int)(0.5 * h)));
                        int xx = x;
                        x += x_dist;
                        int buurtje = 1;
                        for (int z = t + 1; (z < parent.GetDungeon().nodes.Length && z < t + parent.GetDungeon().interval); z++)
                        {
                            if (parent.GetDungeon().nodes[z] != null)
                            {
                                int my_x = 0;
                                int my_y = 0;
                                switch (buurtje) // decides location of node
                                {
                                    case 1:
                                        {
                                            my_x = xx + (int)(0.25 * x_dist);
                                            my_y = y - y_dist;
                                            break;
                                        }
                                    case 2:
                                        {
                                            my_x = xx + (int)(0.25 * x_dist);
                                            my_y = y + y_dist;
                                            break;
                                        }
                                    case 3:
                                        {
                                            my_x = xx + (int)(0.75 * x_dist);
                                            my_y = y - y_dist;
                                            break;
                                        }
                                    case 4:
                                        {
                                            my_x = xx + (int)(0.75 * x_dist);
                                            my_y = y + y_dist;
                                            break;
                                        }
                                    case 5:
                                        {
                                            my_x = xx + (int)(0.60 * x_dist);
                                            my_y = y + y_dist * 2;
                                            break;
                                        }
                                    case 6:
                                        {
                                            my_x = xx + (int)(0.60 * x_dist);
                                            my_y = y - y_dist * 2;
                                            break;
                                        }
                                    default: { Console.WriteLine("WTF!"); break; }
                                }

                                locations.Add(z, new Tuple<int, int>(my_x + (int)+(0.5 * w), my_y + (int)(0.5 * h)));
                                buurtje++;
                            }
                        }
                    }
                }
            }
        }

        private void CheckMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            int pos = parent.GetPlayer().get_position();

            int[] buren = parent.GetState().GetDungeon().GetNode(pos).get_Adj();
            for (int t = 0; t < buren.Length; t++)
            {
                int b_x = locations[buren[t]].Item1 + (int)(0.5 * w);
                int b_y = locations[buren[t]].Item2 + (int)(0.5 * h);

                if (Math.Abs(b_x - x) < 0.5 * w &&
                    Math.Abs(b_y - y) < 0.5 * h)
                {
                    Console.WriteLine("Verplaats speler naar buur " + buren[t]);
                    parent.PlayerMoved(buren[t]);
                    if (parent.GetState().CheckFinished())
                    {
                        parent.NotifyFinished();
                    }
                }
            }
        }

        private void Gamescherm_Load(object sender, EventArgs e)
        {
            Invalidate();
        }

        public bool Save()
        {
            this.Hide();
            string filename = "filename";
            bool saved = false;
            DialogResult r = MessageBox.Show("Do you wish to save your progress?", "LEVEL COMPLETED!", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = filename;
                sfd.DefaultExt = ".text";
                sfd.Filter = "Text documents (.txt)|*.txt";
                sfd.OverwritePrompt = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    parent.Save(sfd.FileName);
                    saved = true;
                }
            }
            this.Show();
            return saved;
        }

        public void ShowUitgespeeld()
        {
            this.Hide();
            MessageBox.Show("Je hebt het spel uitgespeeld!", "GEFELICITEERD!", MessageBoxButtons.OK);
        }

    }
}

