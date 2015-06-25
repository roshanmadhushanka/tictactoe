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
    public partial class InputDialog : Form
    {
        public static string userName;
        public static string ipAddress;
        public static InputDialog frmInputDialog;
        private InputDialog()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            userName = txtUserName.Text;
            ipAddress = txtIPAddress.Text;
            frmInputDialog.Dispose();
        }

        public static void show()
        {
            frmInputDialog = new InputDialog();
            frmInputDialog.ShowDialog();
        }

        public static Boolean isEmpty()
        {
            if (userName == "" || ipAddress == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
