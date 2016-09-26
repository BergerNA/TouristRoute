namespace Tourist
{
    partial class DialogMod
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
            this.labelDialogMod = new System.Windows.Forms.Label();
            this.textBoxDialogMod = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.numericUpDownDialogMod = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDialogMod)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDialogMod
            // 
            this.labelDialogMod.AutoSize = true;
            this.labelDialogMod.Location = new System.Drawing.Point(46, 36);
            this.labelDialogMod.Name = "labelDialogMod";
            this.labelDialogMod.Size = new System.Drawing.Size(35, 13);
            this.labelDialogMod.TabIndex = 0;
            this.labelDialogMod.Text = "label1";
            // 
            // textBoxDialogMod
            // 
            this.textBoxDialogMod.Location = new System.Drawing.Point(102, 33);
            this.textBoxDialogMod.Name = "textBoxDialogMod";
            this.textBoxDialogMod.Size = new System.Drawing.Size(138, 20);
            this.textBoxDialogMod.TabIndex = 1;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(49, 76);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(165, 76);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // numericUpDownDialogMod
            // 
            this.numericUpDownDialogMod.Location = new System.Drawing.Point(102, 33);
            this.numericUpDownDialogMod.Name = "numericUpDownDialogMod";
            this.numericUpDownDialogMod.Size = new System.Drawing.Size(138, 20);
            this.numericUpDownDialogMod.TabIndex = 4;
            // 
            // DialogMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 132);
            this.Controls.Add(this.numericUpDownDialogMod);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxDialogMod);
            this.Controls.Add(this.labelDialogMod);
            this.Name = "DialogMod";
            this.Text = "Change value";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDialogMod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button ButtonCancel;
        public System.Windows.Forms.Label labelDialogMod;
        public System.Windows.Forms.TextBox textBoxDialogMod;
        public System.Windows.Forms.NumericUpDown numericUpDownDialogMod;
    }
}