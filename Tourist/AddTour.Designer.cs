namespace Tourist
{
    partial class AddTour
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CountryBox = new System.Windows.Forms.ComboBox();
            this.RegionBox = new System.Windows.Forms.ComboBox();
            this.TourNameBox = new System.Windows.Forms.TextBox();
            this.DayBox = new System.Windows.Forms.NumericUpDown();
            this.CostsBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.LabelCountry = new System.Windows.Forms.Label();
            this.LabelRegion = new System.Windows.Forms.Label();
            this.LabelTourName = new System.Windows.Forms.Label();
            this.LabelCosts = new System.Windows.Forms.Label();
            this.labelAddTourError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Country:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Region:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Day:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Costs:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tour name:";
            // 
            // CountryBox
            // 
            this.CountryBox.FormattingEnabled = true;
            this.CountryBox.Location = new System.Drawing.Point(101, 17);
            this.CountryBox.Name = "CountryBox";
            this.CountryBox.Size = new System.Drawing.Size(121, 21);
            this.CountryBox.TabIndex = 6;
            // 
            // RegionBox
            // 
            this.RegionBox.FormattingEnabled = true;
            this.RegionBox.Location = new System.Drawing.Point(101, 44);
            this.RegionBox.Name = "RegionBox";
            this.RegionBox.Size = new System.Drawing.Size(121, 21);
            this.RegionBox.TabIndex = 7;
            // 
            // TourNameBox
            // 
            this.TourNameBox.Location = new System.Drawing.Point(101, 71);
            this.TourNameBox.Name = "TourNameBox";
            this.TourNameBox.Size = new System.Drawing.Size(121, 20);
            this.TourNameBox.TabIndex = 8;
            // 
            // DayBox
            // 
            this.DayBox.Location = new System.Drawing.Point(101, 97);
            this.DayBox.Name = "DayBox";
            this.DayBox.Size = new System.Drawing.Size(121, 20);
            this.DayBox.TabIndex = 9;
            this.DayBox.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // CostsBox
            // 
            this.CostsBox.Location = new System.Drawing.Point(101, 123);
            this.CostsBox.Name = "CostsBox";
            this.CostsBox.Size = new System.Drawing.Size(121, 20);
            this.CostsBox.TabIndex = 10;
            this.CostsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CostsBox_KeyPress);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(191, 159);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 12;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(52, 159);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 13;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // LabelCountry
            // 
            this.LabelCountry.AutoSize = true;
            this.LabelCountry.ForeColor = System.Drawing.Color.Red;
            this.LabelCountry.Location = new System.Drawing.Point(229, 20);
            this.LabelCountry.Name = "LabelCountry";
            this.LabelCountry.Size = new System.Drawing.Size(0, 13);
            this.LabelCountry.TabIndex = 14;
            // 
            // LabelRegion
            // 
            this.LabelRegion.AutoSize = true;
            this.LabelRegion.ForeColor = System.Drawing.Color.Red;
            this.LabelRegion.Location = new System.Drawing.Point(229, 47);
            this.LabelRegion.Name = "LabelRegion";
            this.LabelRegion.Size = new System.Drawing.Size(0, 13);
            this.LabelRegion.TabIndex = 15;
            // 
            // LabelTourName
            // 
            this.LabelTourName.AutoSize = true;
            this.LabelTourName.ForeColor = System.Drawing.Color.Red;
            this.LabelTourName.Location = new System.Drawing.Point(229, 74);
            this.LabelTourName.Name = "LabelTourName";
            this.LabelTourName.Size = new System.Drawing.Size(0, 13);
            this.LabelTourName.TabIndex = 16;
            // 
            // LabelCosts
            // 
            this.LabelCosts.AutoSize = true;
            this.LabelCosts.ForeColor = System.Drawing.Color.Red;
            this.LabelCosts.Location = new System.Drawing.Point(229, 126);
            this.LabelCosts.Name = "LabelCosts";
            this.LabelCosts.Size = new System.Drawing.Size(0, 13);
            this.LabelCosts.TabIndex = 17;
            // 
            // labelAddTourError
            // 
            this.labelAddTourError.AutoSize = true;
            this.labelAddTourError.ForeColor = System.Drawing.Color.Red;
            this.labelAddTourError.Location = new System.Drawing.Point(66, 143);
            this.labelAddTourError.Name = "labelAddTourError";
            this.labelAddTourError.Size = new System.Drawing.Size(191, 13);
            this.labelAddTourError.TabIndex = 18;
            this.labelAddTourError.Text = "Тур с такими данными существует! ";
            this.labelAddTourError.Visible = false;
            // 
            // AddTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 197);
            this.Controls.Add(this.labelAddTourError);
            this.Controls.Add(this.LabelCosts);
            this.Controls.Add(this.LabelTourName);
            this.Controls.Add(this.LabelRegion);
            this.Controls.Add(this.LabelCountry);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.CostsBox);
            this.Controls.Add(this.DayBox);
            this.Controls.Add(this.TourNameBox);
            this.Controls.Add(this.RegionBox);
            this.Controls.Add(this.CountryBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddTour";
            this.Text = "New tour";
            this.Load += new System.EventHandler(this.AddTour_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label LabelCountry;
        private System.Windows.Forms.Label LabelRegion;
        private System.Windows.Forms.Label LabelTourName;
        private System.Windows.Forms.Label LabelCosts;
        public System.Windows.Forms.Button AddButton;
        public System.Windows.Forms.ComboBox CountryBox;
        public System.Windows.Forms.ComboBox RegionBox;
        public System.Windows.Forms.TextBox TourNameBox;
        public System.Windows.Forms.NumericUpDown DayBox;
        public System.Windows.Forms.TextBox CostsBox;
        private System.Windows.Forms.Label labelAddTourError;
    }
}