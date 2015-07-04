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
        public Start()
        {
            InitializeComponent();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            if(cmbProfile.Items.Count != 0 && cmbProfile.SelectedItem != null)
            {
                Player tmp = new Player();
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
    }
}
