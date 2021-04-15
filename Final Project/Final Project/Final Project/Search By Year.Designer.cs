namespace Final_Project
{
    partial class Search_By_Year
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
            this.yearBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year:";
            // 
            // yearBox
            // 
            this.yearBox.Location = new System.Drawing.Point(84, 15);
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(141, 22);
            this.yearBox.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.searchButton.Location = new System.Drawing.Point(132, 50);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(93, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // Search_By_Year
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 85);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.label1);
            this.Name = "Search_By_Year";
            this.Text = "Search_By_Year";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox yearBox;
        private System.Windows.Forms.Button searchButton;
    }
}