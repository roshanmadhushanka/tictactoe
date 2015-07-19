using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeGame.DAO;
using TicTacToeGame.DTO;

namespace TicTacToeGame
{
    public partial class Start : Form
    {
        public static Start form { set; get; }
        private List<PlayerDTO> playerList;
        private static Player tmp;
        public Start()
        {
            InitializeComponent();
            Start.form = this;
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            if(cmbProfile.Items.Count != 0 && cmbProfile.SelectedItem != null)
            {
                tmp = new Player();
                tmp.id = playerList[cmbProfile.SelectedIndex].id;
                tmp.name = playerList[cmbProfile.SelectedIndex].name;
                this.Hide();
                new PlayerForm().Show();
            } 
        }

        private void Start_Load(object sender, EventArgs e)
        {
            PlayerDAO playerDAO = new PlayerDAO();
            playerList = playerDAO.selectAll();

            for (int i = 0; i < playerList.Count; i++)
            {
                cmbProfile.Items.Add(playerList[i].name);
            }
        }

        public static Player getLoadPlayer()
        {
            tmp.playerType = PlayerType.BALL;
            tmp.moveAllowed = true;
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
                tmp.id = new PlayerDAO().count()+1;
                PlayerDAO playerDAO = new PlayerDAO();
                playerDAO.create(tmp);
                txtNewPlayer.Text = "";
                this.Hide();
                new PlayerForm().Show();

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
