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
    public partial class ClientGUI : Form
    {
        EasyNetwork.Client client;
        public ClientGUI()
        {
            InitializeComponent();
        }

        private void ClientGUI_Load(object sender, EventArgs e)
        {
          client = new EasyNetwork.Client("tcp://localhost:1982");

            client.Start();
        }

        private void newWorld_Click(object sender, EventArgs e)
        {
            GameWorld.GameWorld temp = new GameWorld.GameWorld();
            Random thias = new Random();
            int fourdigit = thias.Next(0001, 9999);
            textBox1.Text += (fourdigit.ToString() + "\r\n");
            temp.setGameId(fourdigit.ToString());
            textBox1.Text += (temp.getGameId() + "\r\n");
            client.Send(temp);

        }
    }
}
