using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Scrabble.Server
{
    public partial class ServerGUI : Form
    {
        EasyNetwork.Server server;
        public ServerGUI()
        {
            InitializeComponent();
            server = new EasyNetwork.Server("tcp://*:1982");
            server.DataReceived += Server_DataReceived;
            server.Start();
            textBox1.Text += ("Server has started.\r\n");
            
        }
        public void addGameToList(GameWorld.GameWorld newWorld)
        {
            EasyNetwork.Client dummy = new EasyNetwork.Client("tcp://localhost:1982");
            dummy.DataReceived += Client_DataReceived;
            dummy.Start();
            textBox1.Text += ("Server Client List: \r\n");
            try
            {
                for (int i = 1; i <= 1; ++i)
                {
                    textBox1.Text += server.ClientList.ElementAt(i).ToString();
                }
            }
            catch(Exception e)
            {
                textBox1.Text += ("ERROR: " + e.ToString());
            }

            dummy.Send(new Scrabble.ChatPacket(1020,"shane","Hello"));
            gameList.Items.Add(newWorld);
        }

        private void Client_DataReceived(object receivedObject)
        {
            throw new NotImplementedException();
        }

        private void addgamebutton_Click(object sender, EventArgs e)
        {
            addGameToList(new GameWorld.GameWorld());
        }

        private void ServerGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }

        private void Server_DataReceived(object receivedObject, Guid clientId)
       {
           try {
               //process received object
               if (receivedObject is GameWorld.GameWorld)
               {
                   GameWorld.GameWorld clientObj = (GameWorld.GameWorld)receivedObject;
                    textBox1.Text += (clientObj.getGameId());
                    if(clientObj.getGameId() == null)
                    {

                    }
                   textBox1.Text += ("Received GameWorld: " + clientObj.getGameId() + "\r\n");
               }

               if(receivedObject is Scrabble.ChatPacket)
                {
                    Scrabble.ChatPacket clientChat = (Scrabble.ChatPacket)receivedObject;
                    textBox1.Text += ("Received Chat from: " + clientId.ToString() + "\r\n" +
                                        "to Gameworld: " + "GameWorld \r\n");
                }
           }
           catch(NotImplementedException e)
           {
               textBox1.AppendText(e.ToString());
           }
       }
    }
}
