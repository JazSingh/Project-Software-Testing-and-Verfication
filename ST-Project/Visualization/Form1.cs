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

            Stack<Node> path = d.ShortestPath(d.nodes[0], d.nodes[d.nodes.Length-1]);

            Console.WriteLine("Path:");
            while (path.Count > 0)
            {
                Console.WriteLine(path.Pop().ID);
            }

        }
    }
}
