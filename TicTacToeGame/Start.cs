using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public partial class Start : Form
    {
        private List<string>[] playerList;
        private Player tmp;
        public Start()
        {
            InitializeComponent();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            if(cmbProfile.Items.Count != 0 && cmbProfile.SelectedItem != null)
            {
                tmp = new Player();
                tmp.id = Int32.Parse(playerList[0][cmbProfile.SelectedIndex]);
                tmp.name = playerList[1][cmbProfile.SelectedIndex];

                this.Hide();
                new PlayerForm().Show();
            } 
        }

        private void Start_Load(object sender, EventArgs e)
        {
            DatabaseHandler handler = new DatabaseHandler();
            playerList = handler.SelectAllPlayers();

            for (int i = 0; i < playerList[0].Count; i++)
            {
                cmbProfile.Items.Add(playerList[1][i]);
            }
        }

        public Player getLoadPlayer()
        {
            return tmp;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (txtNewPlayer.Text == "")
            {
                MessageBox.Show("Please enter player name!");
            }
            else
            {
                tmp = new Player();
                tmp.name = txtNewPlayer.Text;
                DatabaseHandler handler = new DatabaseHandler();
                handler.save(tmp);
                txtNewPlayer.Text = "";

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
