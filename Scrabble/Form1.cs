using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EasyNetwork;

namespace Scrabble
{
    public partial class Form1 : Form
    {
        EasyNetwork.Client client = new EasyNetwork.Client("tcp://localhost:9001");

        public class tester
        {
            public tester(string s)
            {
                this.tosend = s;
            }

            public string tosend;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private Scrabble.Logger logger;

        private void button1_Click(object sender, EventArgs e)
        {
            logger = new Scrabble.Logger();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EasyNetwork.Server server = new EasyNetwork.Server("tcp://*:9001");
            server.DataReceived += Server_DataReceived;
            server.Start();
            
        }

        private void Server_DataReceived(object receivedObject, Guid clientId)
        {
            try {
                //process received object
                if (receivedObject is tester)
                {
                    tester clientObj = (tester)receivedObject;
                    textBox1.Text += ("Look what I got. A tester object that contains the string " + clientObj.tosend.ToString() + ".\n");
                }
            }
            catch(NotImplementedException e)
            {
                textBox1.AppendText(e.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string message = textBox2.ToString();
            tester test = new tester(message);
            client.Send<tester>(test);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.DataReceived += Client_DataReceived;
            client.Start();
        }

        private void Client_DataReceived(object receivedObject)
        {
            throw new NotImplementedException();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            logger.LogMessage("This is a test message.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            logger.LogWarning("This is a test warning.", "form1 button6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            logger.LogError("This is a test error.", "form1 button7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            logger.AddToGameState("This goes to gamestate.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Scrabble.Game_Objects.GameState state = new Game_Objects.GameState();
            PlayerClass.Player player = new PlayerClass.Player(0001, "username");
            state.AddPlayer(player);
            logger.AddToGameState(state.ToString());
        }
    }
}
