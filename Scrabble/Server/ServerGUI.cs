using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Scrabble.Server
{
    public partial class ServerGUI : Form
    {
        EasyNetwork.Server server;
        Logger Log = new Logger();
        public ServerGUI()
        {
            InitializeComponent();
            server = new EasyNetwork.Server("tcp://*:1982");
            server.DataReceived += Server_DataReceived;
            server.Start();
            comBox.Text += ("Server has started.\r\n");
            Log.LogMessage("Server has started.\r\n");     
        }

        private void Client_DataReceived(object receivedObject)
        {
            throw new NotImplementedException();
        }

        private void ServerGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.LogMessage("Server Closed.");
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
                    Log.LogMessage("Received GameWorld: " + clientObj.GameId + "\r\n");
                    gameList.Items.Add(clientObj);
               }

               if(receivedObject is Scrabble.ChatPacket)
                {
                    Scrabble.ChatPacket clientChat = (Scrabble.ChatPacket)receivedObject;

                   
                    threadSafeToComBox("Received Chat from: " + clientId.ToString() + " " +
                                       "from Gameworld: " + clientChat.GameId +"\r\n");
                    Log.LogMessage("Received Chat from: " + clientId.ToString() + " " +
                                       "from Gameworld: " + clientChat.GameId + "\r\n");
                    chatToClients(clientChat);                   
                }
           }
           catch(NotImplementedException e)
           {
               comBox.AppendText(e.ToString());
           }
       }

        delegate void threadSafeCallback(string text);

        private void threadSafeToComBox(string message)
        {
            if (this.comBox.InvokeRequired)
            {
                threadSafeCallback d = new threadSafeCallback(threadSafeToComBox);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.comBox.Text += (message);
            }
        }
        public string createNewWorld()
        {
            List<PlayerClass.Player> blah = new List<PlayerClass.Player>();
            PlayerClass.Player tempPlayer = new PlayerClass.Player(0001, "Shane");
            PlayerClass.Player tempPlayerw = new PlayerClass.Player(0001, "Wane");
            PlayerClass.Player tempPlayerp = new PlayerClass.Player(0034, "ppane");
            blah.Add(tempPlayer);
            blah.Add(tempPlayerw);
            blah.Add(tempPlayerp);
            GameWorld.GameWorld clientWorld = new GameWorld.GameWorld(blah);
            Random thias = new Random();
            int fourdigit = thias.Next(0001, 9999);
            clientWorld.GameId = fourdigit.ToString();
            threadSafeToComBox("Game World created. \r\nGame ID: ");
            gameList.Items.Add(clientWorld);
            return clientWorld.GameId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string game = createNewWorld();
            threadSafeToComBox(game + "\r\n");
        }

        public List<GameWorld.GameWorld> getGames()
        {
            return this.gameList.Items.OfType<GameWorld.GameWorld>().ToList();
        }
        public void listGames()
        {
            List<GameWorld.GameWorld> tempList = getGames();
            foreach (GameWorld.GameWorld item in tempList)
            {
                threadSafeToComBox("Game ID: " + item.GameId + "\r\n");
            }
        }

        private void printgames_Click(object sender, EventArgs e)
        {
            listGames();
        }
        public void messageToClients(string serverMessage)
        {
            foreach(Guid item in this.server.ClientList)
            {
                server.Send(serverMessage, item);
            }
        }

        public void chatToClients(ChatPacket p)
        {
            foreach (Guid item in this.server.ClientList)
            {
                threadSafeToComBox("Sending to: " + item.ToString() + "\r\n");
                server.Send(p, item);
            }
        }

        private void toClient_Click(object sender, EventArgs e)
        {
            ChatPacket sendPacket = new ChatPacket(0000, "Server", "Welcome to Scrabble!", "0");
            chatToClients(sendPacket);
        }
    }
}
