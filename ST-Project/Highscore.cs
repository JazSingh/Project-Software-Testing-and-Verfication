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
    public partial class Highscore : Form
    {
        GameManager parent;
        Tuple<string, int>[] ns;
        Label[] scores;
        Label[] names;

        public Highscore(GameManager p, Tuple<string, int>[] sc)
        {
            InitializeComponent();
            this.Width = 1280;
            this.Height = 720;
            this.Paint +=Highscore_Paint;

            this.parent = p;
            this.ns = sc;

            names = new Label[] {p10, p9, p8, p7, p6, p5, p4, p3, p2, p1};
            scores = new Label[] {s10, s9, s8, s7, s6, s5, s4, s3, s2, s1};
        }

        void Highscore_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                names[9 - i].Text = ns[i].Item1;
                scores[9 - i].Text = ns[i].Item2.ToString();
            }
        }
    }
}
