using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworksApi.TCP.CLIENT;
using NetworksApi.TCP.SERVER;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml;

namespace TicTacToeGame
{
    public delegate void changeGame(String command);
    public delegate void changeList(String str);

    public partial class PlayerForm : Form
    {
        Game game;
        String clientName;
        Image icon_ball = (Image)Properties.Resources.Ball;
        Image icon_cross = (Image)Properties.Resources.Cross;

        Client client;
        Server server;

        bool playerA = true;

        public void initGame(GameMode gameMode)
        {
            //Initialize game for default state
            Player playerA = new Player(PlayerType.BALL);
            Player playerB = new Player(PlayerType.CROSS);
            game = new Game(playerA, playerB, Difficulty.NORMAL, gameMode);
            game.chanceOfPlayerAI = true;
            game.chanceOfPlayerA = false;
            lblPlayerA.Text = game.playerA.name;
            lblPlayerB.Text = game.playerB.name;
            update();
        }

        public void updateGame(String command)
        {
            //This function used in multiplayer environment to ensure the game is
            //updated by the thread that created the game

            //Normally this method will called on data received
            if (lstMessage.InvokeRequired)
            {
                Invoke(new changeGame(updateGame), new object[] { command });
            }
            else
            {
                lstMessage.Items.Add(command);
                executeCommand(command);
                updateGameDescription();        
                game.moveAllowed = true;                 //Allowed movements for the player
            }
        }

        public void updateList(String str)
        {
            //This function used in multiplayer environment to ensure the list is
            //updated by the thread that created the game

            //Normally this method will called on data received
            if (lstMessage.InvokeRequired)
            {
                Invoke(new changeList(updateList), new object[] { str });
            }
            else
            {
                lstMessage.Items.Add(str);
            }
        }

        private void updateGameDescription()
        {
            //Update gam descriptions 
            lblDifficulty.Text = game.difficulty.ToString();
            lblGameMode.Text = game.game_mode.ToString();
            lblCrossScore.Text = game.playerA.score.ToString();
            lblBallScore.Text = game.playerB.score.ToString();
            //lblPlayerA.Text = game.playerA.name;
            //lblPlayerB.Text = game.playerB.name;
        }
        
        public void resetBoard()
        {
            //Clear board
            //Game remains as it is
            game.resetGameBoard();
            pic00.Image = null;
            pic01.Image = null;
            pic02.Image = null;
            pic10.Image = null;
            pic11.Image = null;
            pic12.Image = null;
            pic20.Image = null;
            pic21.Image = null;
            pic22.Image = null;
            if (game.game_mode == GameMode.SINGLE_PLAYER && game.playerB.moveAllowed)
            {
                game.current_player = game.playerB;
                AI ai = new AI(ref game);
                ai.makeMove();
            }
            else if (game.game_mode == GameMode.SINGLE_PLAYER && game.playerA.moveAllowed)
            {
                game.current_player = game.playerA;
            }
            update();
        }

        public void newGame(GameMode gameMode)
        {
            //Start new game
            initGame(gameMode);
            resetBoard();
            updateGameDescription();
            lstMessage.Items.Clear();
        }

        public PlayerForm()
        {
            InitializeComponent();
            newGame(GameMode.SINGLE_PLAYER);
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {

        }

        private void update()
        {
            //Draw the board
            //pic00
            if (game.board[0, 0] == PlayerType.BALL)
            {
                pic00.Image = icon_ball;
            }
            else if (game.board[0, 0] == PlayerType.CROSS)
            {
                pic00.Image = icon_cross;
            }
            //pic01
            if (game.board[0, 1] == PlayerType.BALL)
            {
                pic01.Image = icon_ball;
            }
            else if (game.board[0, 1] == PlayerType.CROSS)
            {
                pic01.Image = icon_cross;
            }
            //pic02
            if (game.board[0, 2] == PlayerType.BALL)
            {
                pic02.Image = icon_ball;
            }
            else if (game.board[0, 2] == PlayerType.CROSS)
            {
                pic02.Image = icon_cross;
            }
            //pic10
            if (game.board[1, 0] == PlayerType.BALL)
            {
                pic10.Image = icon_ball;
            }
            else if (game.board[1, 0] == PlayerType.CROSS)
            {
                pic10.Image = icon_cross;
            }
            //pic11
            if (game.board[1, 1] == PlayerType.BALL)
            {
                pic11.Image = icon_ball;
            }
            else if (game.board[1, 1] == PlayerType.CROSS)
            {
                pic11.Image = icon_cross;
            }
            //pic12
            if (game.board[1, 2] == PlayerType.BALL)
            {
                pic12.Image = icon_ball;
            }
            else if (game.board[1, 2] == PlayerType.CROSS)
            {
                pic12.Image = icon_cross;
            }
            //pic20
            if (game.board[2, 0] == PlayerType.BALL)
            {
                pic20.Image = icon_ball;
            }
            else if (game.board[2, 0] == PlayerType.CROSS)
            {
                pic20.Image = icon_cross;
            }
            //pic21
            if (game.board[2, 1] == PlayerType.BALL)
            {
                pic21.Image = icon_ball;
            }
            else if (game.board[2, 1] == PlayerType.CROSS)
            {
                pic21.Image = icon_cross;
            }
            //pic22
            if (game.board[2, 2] == PlayerType.BALL)
            {
                pic22.Image = icon_ball;
            }
            else if (game.board[2, 2] == PlayerType.CROSS)
            {
                pic22.Image = icon_cross;
            }

            //Score update

            updateGameDescription();
        }

        private void makeMove(int x, int y)
        {
            if (game.game_mode == GameMode.SINGLE_PLAYER)
            {
                if (game.playerA.moveAllowed)
                {
                    game.playerA.move(x, y);
                    update();
                    if (game.isGameFinished())
                    {
                        MessageBox.Show(game.getGameStat().ToString());
                        update();
                        resetBoard();
                    }
                    else
                    {
                        AI ai = new AI(ref game);
                        ai.makeMove();
                        update();
                        if (game.isGameFinished())
                        {
                            MessageBox.Show(game.getGameStat().ToString());
                            update();
                            resetBoard();
                        }
                    }
                }                
            }
            else if(game.game_mode == GameMode.MULTI_PLAYER)
            {
                //Multiplayer game movements 
                if (game.moveAllowed && game.connected)
                {
                    String str;
                    if (server != null && game.playerA.move(x, y))
                    {
                        str = generateStringCommand(PlayerType.BALL, x, y);
                        sendCommand(str);
                    }
                    else if (client != null && game.playerB.move(x, y))
                    {
                        str = generateStringCommand(PlayerType.CROSS, x, y);
                        sendCommand(str);
                    }

                    update();
                    game.moveAllowed = false;

                    if (game.isGameFinished())
                    {
                        MessageBox.Show(game.getGameStat().ToString());
                        updateGameDescription();
                        resetBoard();
                        update();
                        if (playerA == false && client != null)
                        {
                            playerA = true;
                            game.moveAllowed = true;
                        }else if(playerA == false && server != null){
                            playerA = true;
                            game.moveAllowed = false;
                        }
                        else if (playerA == true && server != null)
                        {
                            playerA = false;
                            game.moveAllowed = false;
                        }
                        else if (playerA == true && server != null)
                        {
                            playerA = true;
                            game.moveAllowed = true;
                        }
                    }
                }
            }else if(game.game_mode == GameMode.MULTI_PLAYER_STANDALONE){
                if(game.playerA.moveAllowed)
                {
                    game.playerA.move(x, y);
                    update();
                }
                else if (game.playerB.moveAllowed)
                {
                    game.playerB.move(x, y);
                    update();
                }
                if (game.isGameFinished())
                {
                    MessageBox.Show(game.getGameStat().ToString());
                    update();
                    resetBoard();
                }
            }
        }

        private void sendCommand(String command)
        {
            //Inform abouth the move though the network
            if (client != null && client.IsConnected)
            {
                client.Send(command);
            }
            else if(server != null){
                server.SendTo(clientName,command);
            }
        }
        
        private String generateStringCommand(PlayerType player, int x, int y)
        {
            return player.ToString() + x.ToString() + y.ToString();
        }

        private void executeCommand(String str)
        {
            //Recieve the movements of other player and update the game
            String player;
            int x;
            int y;

            if (str.Length == 7)
            {
                player = str.Substring(0, 5);
                x = Convert.ToInt32(str[5]) - 48;
                y = Convert.ToInt32(str[6]) - 48;
                game.playerB.move(x, y);
            }
            else if (str.Length == 6)
            {
                player = str.Substring(0, 4);
                x = Convert.ToInt32(str[4]) - 48;
                y = Convert.ToInt32(str[5]) - 48;
                game.playerA.move(x, y);
            }
            update();
            

            if (game.isGameFinished())
            {
                MessageBox.Show(game.getGameStat().ToString());
                updateGameDescription();
                resetBoard();
                update();
                if (playerA == false && client != null)
                {
                    playerA = true;
                    game.moveAllowed = true;
                }
                else if (playerA == false && server != null)
                {
                    playerA = true;
                    game.moveAllowed = false;
                }
                else if (playerA == true && server != null)
                {
                    playerA = false;
                    game.moveAllowed = false;
                }
                else if (playerA == true && server != null)
                {
                    playerA = true;
                    game.moveAllowed = true;
                }
            }
        }

        private void pic00_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(game.board[0, 0].ToString());
            makeMove(0, 0);
        }

        private void pic01_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(game.board[0, 1].ToString());
            makeMove(0, 1);
        }

        private void pic02_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(game.board[0, 2].ToString());
            makeMove(0, 2);
        }

        private void pic10_Click(object sender, EventArgs e)
        {
            makeMove(1, 0);
        }

        private void pic11_Click(object sender, EventArgs e)
        {
            makeMove(1, 1);
        }

        private void pic12_Click(object sender, EventArgs e)
        {
            makeMove(1, 2);
        }

        private void pic20_Click(object sender, EventArgs e)
        {
            makeMove(2, 0);
        }

        private void pic21_Click(object sender, EventArgs e)
        {
            makeMove(2, 1);
        }

        private void pic22_Click(object sender, EventArgs e)
        {
            makeMove(2, 2);
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame(GameMode.SINGLE_PLAYER);
            game.difficulty = Difficulty.EASY;
            updateGameDescription();
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame(GameMode.SINGLE_PLAYER);
            game.difficulty = Difficulty.NORMAL;
            updateGameDescription();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame(GameMode.SINGLE_PLAYER);
            game.difficulty = Difficulty.HARD;
            updateGameDescription();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame(GameMode.SINGLE_PLAYER);
            game.difficulty = Difficulty.NORMAL;
            updateGameDescription();
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (client == null)
            {
                newGame(GameMode.MULTI_PLAYER);
                game.game_mode = GameMode.MULTI_PLAYER;
                updateGameDescription();

                playerA = true;
                game.moveAllowed = true;
                //moveAllowed = true;
                InputDialog.show();
                if (!InputDialog.isEmpty())
                {
                    server = new Server(InputDialog.ipAddress, "90");
                    server.OnClientConnected += new OnConnectedDelegate(server_OnClientConnected);
                    server.OnClientDisconnected += new OnDisconnectedDelegate(server_OnClientDisconnected);
                    server.OnDataReceived += new OnReceivedDelegate(server_OnDataReceived);
                    server.OnServerError += new OnErrorDelegate(server_OnServerError);
                    server.Start();
                    updateList("Server Name : " + InputDialog.userName);
                    updateList("Server Start : " + InputDialog.ipAddress);
                }
                
            }
        }

        private void server_OnServerError(object Sender, ErrorArguments R)
        {
            
        }

        private void server_OnDataReceived(object Sender, ReceivedArguments R)
        {
            updateGame(R.ReceivedData);
            game.moveAllowed = true;
            update();
        }

        private void server_OnClientDisconnected(object Sender, DisconnectedArguments R)
        {
            game.connected = false;
        }

        private void server_OnClientConnected(object Sender, ConnectedArguments R)
        {
            updateList(R.Ip + " " + R.Name);
            clientName = R.Name;
            game.connected = true;
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                newGame(GameMode.MULTI_PLAYER);
                game.game_mode = GameMode.MULTI_PLAYER;
                updateGameDescription();

                playerA = false;
                game.moveAllowed = false;

                InputDialog.show();
                if (!InputDialog.isEmpty())
                {
                    client = new Client();
                    client.ClientName = InputDialog.userName;
                    client.ServerIp = InputDialog.ipAddress;
                    client.ServerPort = "90";
                    client.OnClientConnected += new OnClientConnectedDelegate(client_OnClientConnected);
                    client.OnClientConnecting += new OnClientConnectingDelegate(client_OnClientConnecting);
                    client.OnClientError += new OnClientErrorDelegate(client_OnClientError);
                    client.OnClientFileSending += new OnClientFileSendingDelegate(client_OnClientFileSending);
                    client.OnDataReceived += new OnClientReceivedDelegate(client_OnDataReceived);
                    client.OnClientDisconnected += new OnClientDisconnectedDelegate(client_OnClientDisconnected);
                    client.Connect();
                    updateList("Client Name : " + InputDialog.userName);
                    updateList("Client Start : " + InputDialog.ipAddress);
                }
            }
        }

        private void client_OnClientDisconnected(object Sender, ClientDisconnectedArguments R)
        {
            game.connected = false;
        }

        private void client_OnDataReceived(object Sender, ClientReceivedArguments R)
        {
            updateGame(R.ReceivedData);
            game.moveAllowed = true;
            update();
        }

        private void client_OnClientFileSending(object Sender, ClientFileSendingArguments R)
        {
           
        }

        private void client_OnClientError(object Sender, ClientErrorArguments R)
        {
            
        }

        private void client_OnClientConnecting(object Sender, ClientConnectingArguments R)
        {
            updateList("Connecting...");
        }

        private void client_OnClientConnected(object Sender, ClientConnectedArguments R)
        {
            updateList("Connected");
            game.connected = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void easyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newGame(GameMode.SINGLE_PLAYER);
            game.difficulty = Difficulty.EASY;
            updateGameDescription();
        }

        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newGame(GameMode.SINGLE_PLAYER);
            game.difficulty = Difficulty.NORMAL;
            updateGameDescription();
        }

        private void hardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newGame(GameMode.SINGLE_PLAYER);
            game.difficulty = Difficulty.HARD;
            updateGameDescription();
        }

        private void crateServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                

                //playerA = true;
                //game.moveAllowed = true;
                //moveAllowed = true;
                InputDialog.show();
                if (!InputDialog.isEmpty())
                {
                    newGame(GameMode.MULTI_PLAYER);
                    game.game_mode = GameMode.MULTI_PLAYER;
                    updateGameDescription();
                    server = new Server(InputDialog.ipAddress, "90");
                    server.OnClientConnected += new OnConnectedDelegate(server_OnClientConnected);
                    server.OnClientDisconnected += new OnDisconnectedDelegate(server_OnClientDisconnected);
                    server.OnDataReceived += new OnReceivedDelegate(server_OnDataReceived);
                    server.OnServerError += new OnErrorDelegate(server_OnServerError);
                    server.Start();
                    updateList("Server Name : " + InputDialog.userName);
                    updateList("Server Start : " + InputDialog.ipAddress);
                }

            }
        }

        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                

                //playerA = false;
                //game.moveAllowed = false;

                InputDialog.show();
                if (!InputDialog.isEmpty())
                {
                    newGame(GameMode.MULTI_PLAYER);
                    game.game_mode = GameMode.MULTI_PLAYER;
                    updateGameDescription();
                    client = new Client();
                    client.ClientName = InputDialog.userName;
                    client.ServerIp = InputDialog.ipAddress;
                    client.ServerPort = "90";
                    client.OnClientConnected += new OnClientConnectedDelegate(client_OnClientConnected);
                    client.OnClientConnecting += new OnClientConnectingDelegate(client_OnClientConnecting);
                    client.OnClientError += new OnClientErrorDelegate(client_OnClientError);
                    client.OnClientFileSending += new OnClientFileSendingDelegate(client_OnClientFileSending);
                    client.OnDataReceived += new OnClientReceivedDelegate(client_OnDataReceived);
                    client.OnClientDisconnected += new OnClientDisconnectedDelegate(client_OnClientDisconnected);
                    client.Connect();
                    updateList("Client Name : " + InputDialog.userName);
                    updateList("Client Start : " + InputDialog.ipAddress);
                }
            }
        }

        private void singlePCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame(GameMode.MULTI_PLAYER_STANDALONE);
            game.game_mode = GameMode.MULTI_PLAYER_STANDALONE;
            updateGameDescription();
        }

    }
}
