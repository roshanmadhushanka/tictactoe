using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworksApi.TCP.SERVER;
using NetworksApi.TCP.CLIENT;

namespace TicTacToeGame
{
    class Network
    {
       
        
        private Network()
        {

        }

        public Server startServer(String ipAddress)
        {
            Server server = null;
            server = new Server(ipAddress, "90");
            server.Start();
            return server;
        }

        public Client connectToServer(String ipAddress)
        {
            Client client = null;
            client = new Client();
            client.ServerIp = ipAddress;
            client.ServerPort = "90";
            client.Connect();
            return client;
        }
    }
}
