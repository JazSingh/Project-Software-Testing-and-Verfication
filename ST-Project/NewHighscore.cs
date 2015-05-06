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
    public partial class NewHighscore : Form
    {
        GameManager parent;
        public NewHighscore(GameManager p)
        {
            InitializeComponent();
            parent = p;
        }

        private void save_b_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == string.Empty) MessageBox.Show("Please enter a name", "Error!", MessageBoxButtons.OK);
            else
            {
                parent.WriteHighscore(textBox1.Text);
                this.Close();
            }
        }
    }
}
