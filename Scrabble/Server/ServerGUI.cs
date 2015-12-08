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
            comBox.Text += ("Server has started.\r\n");        
        }

        private void Client_DataReceived(object receivedObject)
        {
            throw new NotImplementedException();
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
                    if(clientObj.GameId == null)
                    {

                    }
                   comBox.Text += ("Received GameWorld: " + clientObj.GameId + "\r\n");
                    gameList.Items.Add(clientObj);
               }

               if(receivedObject is Scrabble.ChatPacket)
                {
                    Scrabble.ChatPacket clientChat = (Scrabble.ChatPacket)receivedObject;
                    comBox.Text += ("Received Chat from: " + clientId.ToString() + "\r\n" +
                                        "to Gameworld: " + "GameWorld \r\n");
                }
           }
           catch(NotImplementedException e)
           {
               comBox.AppendText(e.ToString());
           }
       }
    }
}
