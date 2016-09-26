using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Tourist
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            this.PassBoxText.Focus();
        }

        private void PassBoxText_TextChanged(object sender, EventArgs e)
        {
            //(e.KeyCode == Keys.Enter)
             //   button1_Click(sender, e);
        }

        private void PassBoxText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        Guid GetHashString(string s)
        {
            //переводим строку в байт-массив  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средств шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return new Guid(hash);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (labelChangePas.Text == "Enter new password" || !File.Exists(Application.StartupPath + @"\\pas.txt"))
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\\pas.txt");
                if (labelChangePas.Text == "Enter new password")
                {
                    sw.WriteLine(GetHashString(PassBoxText.Text));
                    this.Close();
                }
                else sw.WriteLine(ResourceImage.Tests);
                sw.Close();
            }
            string str = null;
            if (File.Exists(Application.StartupPath + @"\\pas.txt"))
            {
                StreamReader re = new StreamReader(Application.StartupPath + @"\\pas.txt");
                str = re.ReadLine();
                re.Close();
                if (GetHashString(this.PassBoxText.Text).ToString() == str)
                {
                    if(this.Name == "Change password")
                    {
                        label1.Visible = false;
                        labelChangePas.Text = "Enter new password";
                        labelChangePas.Visible = true;
                        PassBoxText.Clear();
                    }
                    else
                    {
                        ((Form1)this.Owner).Authorized.ForeColor = Color.Green;
                        //TODO: Включить кнопки администратора для модификации информации
                        Form1 main = this.Owner as Form1;
                        this.Close();
                    }
                }
                else
                {
                    if(labelChangePas.Visible = true)
                        labelChangePas.Visible = false;
                    label1.Text = "Invalid old password";
                    label1.Visible = true;
                    this.PassBoxText.Clear();
                    this.PassBoxText.Focus();
                }
            }
        }
        private void InitializeMyControl()
        {
            this.PassBoxText.Text = "";
            this.PassBoxText.PasswordChar = '*';
            this.PassBoxText.MaxLength = 14;
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            InitializeMyControl();
        }
        
    }
}
