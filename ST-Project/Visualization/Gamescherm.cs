using ST_Project.GameState;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST_Project.Visualization
{
    public partial class Gamescherm : Form
    {
        Dungeon d;

        public Gamescherm(int i)
        {
            InitializeComponent();
            d = new Dungeon(i);
            Paint += teken;
            this.DoubleBuffered = true;
        }

        private void teken(object sender, PaintEventArgs e)
        {

            Dictionary<int, Tuple<int, int>> locations = new Dictionary<int, Tuple<int, int>>();
            Graphics gr = e.Graphics;

            int x = 100;
            int y = 100;
            int h = 30;
            int w = 30;
            int x_dist = 420-(60*(d.difficulty-1));
            int y_dist = 80;

            Node u = d.nodes[0];
            Node v = d.nodes[d.nodes.Length - 1];
            Stack<Node> pad = d.ShortestPath(u, v);

            Font drawFont = new Font("Arial", 16);

            ///////////// draw Dungeon, based on partitions

            // calc node-positions 
            // WARNING: 
            // DON'T LOOK AT THIS CODE, IT'S PURE MAGIC
            // ONLY THE WRITER KNOWS WHAT IT MEANS...
            for (int t = 0; t < d.nodes.Length;t+=d.interval)
            {
                if (d.nodes[t] != null)
                {
                    if (t % d.interval == 0)
                    {
                        locations.Add(t, new Tuple<int, int>(x+(int)(0.5*w), y+(int)(0.5*h)));
                        int xx = x;
                        x += x_dist;
                        int buurtje = 1;
                        for (int z = t+1; (z < d.nodes.Length && z < t + d.interval); z++)
                        {
                            if (d.nodes[z] != null)
                            {
                                int my_x = 0;
                                int my_y = 0;
                                Brush c = Brushes.White;
                                switch (buurtje) // decides location of node
                                {
                                    case 1:
                                        {
                                            my_x = xx + (int)(0.25 * x_dist);
                                            my_y = y - y_dist;
                                            c = Brushes.Green;
                                            break;
                                        }
                                    case 2:
                                        {
                                            my_x = xx + (int)(0.25 * x_dist);
                                            my_y = y + y_dist;
                                            c = Brushes.Orange;
                                            break;
                                        }
                                    case 3:
                                        {
                                            my_x = xx + (int)(0.75 * x_dist);
                                            my_y = y - y_dist;
                                            c = Brushes.Red;
                                            break;
                                        }
                                    case 4:
                                        {
                                            my_x = xx + (int)(0.75 * x_dist);
                                            my_y = y + y_dist;
                                            c = Brushes.Purple;
                                            break;
                                        }
                                    case 5:
                                        {
                                            my_x = xx + (int)(0.50 * x_dist);
                                            my_y = y + y_dist*2;
                                            c = Brushes.Pink;
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

                for (int t = 0; t<d.nodes[key].adj.Length; t++)
                {
                    int buur = d.nodes[key].adj[t];

                    if (d.nodes[key].adj[t] > key)
                    {
                        int x_end = locations[buur].Item1 + (int)(0.5 * w);
                        int y_end = locations[buur].Item2 + (int)(0.5 * h);

                        gr.DrawLine(Pens.Black, x_pos, y_pos, x_end, y_end);
                    }
                }

                // draw node
                Brush color = Brushes.White;
                if (k.Key == 0)
                    color = Brushes.Green;
                else if (k.Key != 0 && k.Key % d.interval == 0)
                    color = Brushes.Orange;
                else if (k.Key == d.nodes.Length - 1)
                    color = Brushes.Red;
                gr.FillEllipse(color, k.Value.Item1, k.Value.Item2, w, h);
                gr.DrawString(k.Key.ToString(), drawFont, Brushes.Black, k.Value.Item1, k.Value.Item2);
            }

                Invalidate();
        }
    }
}

