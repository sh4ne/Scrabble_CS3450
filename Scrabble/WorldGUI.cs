using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble
{
    public partial class WorldGUI : Form
    {
        public WorldGUI()
        {
            InitializeComponent();
            Logger WorldLog = new Logger();
            WorldLog.LogMessage("World GUI Initialized.");
        }

        private void TileTest_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
