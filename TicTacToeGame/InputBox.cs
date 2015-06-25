using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public static class InputBox
    {
        private static System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
        public static string ResultValue;
        private static DialogResult DialogRes;
        private static System.Windows.Forms.TextBox textBox;

        public static DialogResult Show()
        {
            frm.Controls.Clear();
            ResultValue = "";

            //Form
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            frm.Size = new System.Drawing.Size(300, 80);
            frm.Text = "IP Address";
            frm.ShowIcon = false;
            frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
            frm.StartPosition = FormStartPosition.CenterParent;

            //Panel 
            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Size = new System.Drawing.Size(300, 75);
            panel.BackColor = System.Drawing.Color.White;
            

            //Label 
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            label.Text = "IP Address";
            label.Size = new System.Drawing.Size(75, 25);
            label.Location = new System.Drawing.Point(10, 10);
            panel.Controls.Add(label);

            //Text Box
            textBox = new System.Windows.Forms.TextBox();
            textBox.Size = new System.Drawing.Size(180, 25);
            textBox.Location = new System.Drawing.Point(90, 10);
            textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_KeyDown);
            textBox.Name = "textBox";
            panel.Controls.Add(textBox);

            frm.Controls.Add(panel);
            frm.ShowDialog();

            return DialogRes;
        }

        private static void frm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (DialogRes != null) { }
            else DialogRes = DialogResult.None;
        }
        private static void textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResultValue = textBox.Text;
                DialogRes = DialogResult.OK;
                frm.Close();
            }
        }
    }
}
