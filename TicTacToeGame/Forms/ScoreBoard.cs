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

namespace TicTacToeGame.Forms
{
    public partial class ScoreBoard : Form
    {
        public ScoreBoard()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            PlayerForm.form.Show();
        }

        private void populateScoreDataGrid()
        {
            List<ScoreDTO> list = new ScoreDAO().getScore(new PlayerDTO(Start.getLoadPlayer()));
            foreach (var score in list)
            {
                string[] row = { score.score.ToString(), score.difficulty.ToString(), score.game_mode.ToString() };
                dgvScore.Rows.Add(row);
            }
        }

        private void updateDataGrid()
        {
            dgvScore.Rows.Clear();
            int order = cmbOrderBy.SelectedIndex;
           
            List<ScoreDTO> list = new ScoreDAO().getScore(new PlayerDTO(Start.getLoadPlayer()));
            List<ScoreDTO> ordered_list = new List<ScoreDTO>(); 
            switch(order){
                case 0:
                    ordered_list = list.OrderBy(o => o.score).ToList();
                    break;
                case 1:
                    ordered_list = list.OrderBy(o => (int)o.difficulty).ToList();
                    break;
                case 2:
                    ordered_list = list.OrderBy(o => (int)o.game_mode).ToList();
                    break;
                default:
                    ordered_list = list.OrderBy(o => (int)o.score).ToList();
                    break;
            }

            foreach (var score in ordered_list)
            {
                string[] row = { score.score.ToString(), score.difficulty.ToString(), score.game_mode.ToString() };
                dgvScore.Rows.Add(row);
            }
        }

        private void ScoreBoard_Load(object sender, EventArgs e)
        {
            lblPlayer.Text = Start.getLoadPlayer().name;
            updateDataGrid();
        }

        private void cmbOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateDataGrid();
        }

       
    }
}
