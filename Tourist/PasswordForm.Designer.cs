namespace Tourist
{
    partial class PasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.PassBoxText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelChangePas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PassBoxText
            // 
            this.PassBoxText.Location = new System.Drawing.Point(31, 25);
            this.PassBoxText.Name = "PassBoxText";
            this.PassBoxText.PasswordChar = '*';
            this.PassBoxText.Size = new System.Drawing.Size(100, 20);
            this.PassBoxText.TabIndex = 0;
            this.PassBoxText.TextChanged += new System.EventHandler(this.PassBoxText_TextChanged);
            this.PassBoxText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassBoxText_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Invalid password";
            this.label1.Visible = false;
            // 
            // labelChangePas
            // 
            this.labelChangePas.AutoSize = true;
            this.labelChangePas.Location = new System.Drawing.Point(28, 9);
            this.labelChangePas.Name = "labelChangePas";
            this.labelChangePas.Size = new System.Drawing.Size(97, 13);
            this.labelChangePas.TabIndex = 3;
            this.labelChangePas.Text = "Enter old password";
            this.labelChangePas.Visible = false;
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(166, 103);
            this.Controls.Add(this.labelChangePas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PassBoxText);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PasswordForm";
            this.Text = "Administration password";
            this.Load += new System.EventHandler(this.PasswordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox PassBoxText;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label labelChangePas;
    }
}