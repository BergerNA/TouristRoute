using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tourist
{
    public partial class DialogMod : Form
    {
        public DialogMod()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1; // Определяем владельца
            if(textBoxDialogMod.Visible != false)
            {
                main.valueChange = textBoxDialogMod.Text;
            }
            else
            {
                main.dayChange = Convert.ToInt32(numericUpDownDialogMod.Value);
            }
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
