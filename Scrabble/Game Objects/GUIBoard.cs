using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble.GameWorld
{
    public partial class GUIBoard : Form
    {
        EasyNetwork.Client clientGui = new EasyNetwork.Client("tcp://192.168.56.1:1982");
        
        public int GameID = 1214;

        public GUIBoard()
        {
            InitializeComponent();
            clientGui.DataReceived += ClientGui_DataReceived;
            clientGui.Start();
            ChatPacket temp = new ChatPacket(0003, "Client", "Client connected.", this.GameID.ToString());
            clientGui.Send(temp);
        }

        private void ClientGui_DataReceived(object receivedObject)
        {
           
            if (receivedObject is ChatPacket)
            {
                ChatPacket chat = (ChatPacket)receivedObject;
                threadSafeToServerCom(chat.Username + ": " + chat.Message + "\r\n");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GUIBoard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChatPacket toSend = new ChatPacket(0003, "Bobby", ChatLine.Text, this.GameID.ToString());
            clientGui.Send(toSend);
            ChatLine.Clear();
        }
        public int GameId
        {
            get
            {
                return GameID;
            }
        }
        delegate void threadSafeCallback(string text);

        private void threadSafeToServerCom(string message)
        {
            if (this.ServerCom.InvokeRequired)
            {
                threadSafeCallback d = new threadSafeCallback(threadSafeToServerCom);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.ServerCom.Text += (message);
            }
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ChatPacket toSend = new ChatPacket(0003, "Bobby", ChatLine.Text, this.GameID.ToString());
                clientGui.Send(toSend);
                ChatLine.Clear();
            }
        }
    }
}


