using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble.Server
{
    public partial class adminPass : Form
    {
        public adminPass()
        {
            InitializeComponent();
        }

        private void PassSub_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Equals("Scr@bble"))
            {
                this.Close();
                Form server = new Server.ServerGUI();
                server.Show();
            }
            else
            {
                textBox1.Clear();
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals("Scr@bble"))
                {
                    this.Close();
                    Form server = new Server.ServerGUI();
                    server.Show();
                }
                else
                {
                    textBox1.Clear();
                }
            }
        }
    }
}
