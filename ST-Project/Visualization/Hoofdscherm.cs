using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ST_Project.GameState;

namespace ST_Project
{
    public partial class DungeonRPG : Form
    {
        public DungeonRPG()
        {
            InitializeComponent();
            Dungeon d = new Dungeon(5);
        }

        private void DungeonRPG_Load(object sender, EventArgs e)
        {

        }
    }
}
