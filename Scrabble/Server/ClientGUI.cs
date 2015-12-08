using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scrabble.Game_Objects;

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

        private void newWorld_Click(object sender, EventArgs exc)
        {
            try {
                List<PlayerClass.Player> blah = new List<PlayerClass.Player>();
                PlayerClass.Player tempPlayer = new PlayerClass.Player(0001, "Shane");
                PlayerClass.Player tempPlayerw = new PlayerClass.Player(0001, "Wane");
                PlayerClass.Player tempPlayerp = new PlayerClass.Player(0034, "ppane");
                blah.Add(tempPlayer);
                blah.Add(tempPlayerw);
                blah.Add(tempPlayerp);
                GameWorld.GameWorld temp = new GameWorld.GameWorld(blah);
                Random thias = new Random();
                int fourdigit = thias.Next(0001, 9999);
                textBox1.Text += (fourdigit.ToString() + "\r\n");
                temp.GameId = fourdigit.ToString();
                textBox1.Text += (temp.GameId + "\r\n");
                client.Send(temp);
            }
            catch(TurnOrder.InvalidTurnQueueSizeException except)
            {
                textBox1.Text += ("EXCEPTION: " + except.ToString());
            }
           
        }
    }
}
