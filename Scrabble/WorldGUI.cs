using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

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

        private void createServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form server = new Server.ServerGUI();
            server.Show();
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            int xCord = e.X;
            int yCord = e.Y;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form admin = new Server.adminPass();
            admin.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form client = new Server.ClientGUI();
            client.Show();
        }
    }
}
