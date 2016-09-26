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
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            pictureBox1.Image = ResourceImage.Travel_Paralysis_Signpost_Directions;
        }

        private void buttonAboutOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
