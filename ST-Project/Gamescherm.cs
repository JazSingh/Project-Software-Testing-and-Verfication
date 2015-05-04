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
    public partial class Gamescherm : Form
    {
        GameManager parent;

        public Gamescherm(int i, GameManager gs)
        {
            InitializeComponent();
            this.parent = gs;
            Paint += teken;
            this.DoubleBuffered = true;
            this.Height = 720;
            this.Width = 1280;
        }

        public void teken(object sender, PaintEventArgs e)
        {
            Dictionary<int, Tuple<int, int>> locations = new Dictionary<int, Tuple<int, int>>();
            Graphics gr = e.Graphics;

            int x = 100;
            int y = 200;
            int h = 30;
            int w = 30;
            int x_dist = 0;
            switch(parent.GetDungeon().difficulty)
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
                        locations.Add(t, new Tuple<int, int>(x+(int)(0.5*w), y+(int)(0.5*h)));
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
                                            my_y = y + y_dist*2;
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

            // draw Edges
            foreach (KeyValuePair<int, Tuple<int, int>> k in locations)
            {
                int key = k.Key;
                int x_pos = k.Value.Item1+(int)(0.5*w);
                int y_pos = k.Value.Item2+(int)(0.5*h);

                for (int t = 0; t < parent.GetDungeon().nodes[key].adj.Length; t++)
                {
                    int buur = parent.GetDungeon().nodes[key].adj[t];

                    if (parent.GetDungeon().nodes[key].adj[t] > key)
                    {
                        int x_end = locations[buur].Item1 + (int)(0.5 * w);
                        int y_end = locations[buur].Item2 + (int)(0.5 * h);

                        gr.DrawLine(Pens.Black, x_pos, y_pos, x_end, y_end);
                    }
                }

                // draw node
                Brush color = Brushes.White;
                if (k.Key == 0)
                    color = Brushes.Green;              // color of start-node
                else if (k.Key != 0 && k.Key % parent.GetDungeon().interval == 0)
                    color = Brushes.Orange;             // color of Bridge
                else if (k.Key == parent.GetDungeon().nodes.Length - 1)
                    color = Brushes.Red;                // color of end-node

                gr.FillEllipse(color, k.Value.Item1, k.Value.Item2, w, h);
                gr.DrawString(k.Key.ToString(), drawFont, Brushes.Black, k.Value.Item1, k.Value.Item2);
            }

            Invalidate();
        }

        private void Gamescherm_Load(object sender, EventArgs e)
        {

        }
    }
}

