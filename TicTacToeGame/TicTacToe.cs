using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworksApi.TCP.SERVER;

namespace TicTacToeGame
{
    public delegate void updateChatLog(string txt);
    public delegate void updateListBox(ListBox listBox, string val, bool remove);
    public partial class TicTacToe : Form
    {
        Server server;
        public TicTacToe()
        {
            InitializeComponent();
        }

        public void changeLog(string txt)
        {
            if (txt_A_Name.InvokeRequired)
            {
                Invoke(new updateChatLog(changeLog), new object[] { txt });
            }
            else
            {
                txt_A_Name.Text = txt + "\r\n";
            }
        }

        public void changeListBox(ListBox listBox, string val, bool remove)
        {
            if (listBox.InvokeRequired)
            {
                Invoke(new updateListBox(changeListBox), new object[] { listBox, val, remove });
            }
            else
            {
                if (remove)
                {
                    listBox.Items.Remove(val);
                }
                else
                {
                    listBox.Items.Add(val);
                }

            }
        }
        private void TicTacToe_Load(object sender, EventArgs e)
        {
            
        }

        private void server_OnServerError(object Sender, ErrorArguments R)
        {
            
        }

        private void server_OnDataReceived(object Sender, ReceivedArguments R)
        {
            
        }

        private void server_OnClientDisconnected(object Sender, DisconnectedArguments R)
        {
            
        }

        private void server_OnClientConnected(object Sender, ConnectedArguments R)
        {
            changeLog(R.Ip);
            if (Game.current_game.playerA.ip_address == "")
            {
                Game.current_game.playerA.ip_address = R.Ip;
                changeLog(R.Ip);
            }
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            server = new Server("192.168.236.1", "90");
            server.OnClientConnected += new OnConnectedDelegate(server_OnClientConnected);
            server.OnClientDisconnected += new OnDisconnectedDelegate(server_OnClientDisconnected);
            server.OnDataReceived += new OnReceivedDelegate(server_OnDataReceived);
            server.OnServerError += new OnErrorDelegate(server_OnServerError);
            server.Start();
        }
    }
}
