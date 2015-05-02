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

            int x = 50;
            int y = 50;
            int h = 30;
            int w = 30;
            int dist = 120;

            Node u = d.nodes[0];
            Node v = d.nodes[d.nodes.Length - 1];
            Stack<Node> pad = d.ShortestPath(u, v);

            Font drawFont = new Font("Arial", 16);

            // draw start-partitions

            for (int t = 0; t < d.nodes.Length;t++)
            {
                if (d.nodes[t] != null)
                {
                    if (t % d.interval == 0)
                    {
                        gr.FillEllipse(Brushes.Yellow, x, y, h, w);
                        gr.DrawString(t.ToString(), drawFont, Brushes.Black, x, y);
                        int xx = x;
                        x += dist;
                        int buurtje = 1;
                        for (int z = t; (z < d.nodes.Length && z < t + d.interval); z++)
                        {
                            int my_x = 0;
                            int my_y = 0;
                            switch (buurtje)
                            {
                                case 1:
                                    {
                                        my_x = xx + (int)0.25 * dist;
                                        my_y = y -= 30;
                                        break;
                                    }
                                case 2:
                                    {
                                        my_x = xx + (int)0.25 * dist;
                                        my_y = y += 30;
                                        break;
                                    }
                                case 3:
                                    {
                                        my_x = xx + (int)0.75 * dist;
                                        my_y = y -= 30;
                                        break;
                                    }
                                case 4:
                                    {
                                        my_x = xx + (int)0.75 * dist;
                                        my_y = y += 30;
                                        break;
                                    }
                                case 5:
                                    {
                                        my_x = xx + (int)0.50 * dist;
                                        my_y = y += 60;
                                        break;
                                    }
                                default: { Console.WriteLine("WTF!"); break; }
                            }

                            gr.FillEllipse(Brushes.Red, my_x, my_y, w, h);
                            gr.DrawString(d.nodes[z].ID.ToString(), drawFont, Brushes.Black, new Point(my_x, my_y));
                            buurtje++;
                        }
                    }
                }
            }

            Invalidate();
        }
    }
}

