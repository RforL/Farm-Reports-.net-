namespace Final_Project
{
    partial class Search_By_ID
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
            this.ID = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID.Location = new System.Drawing.Point(12, 9);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(40, 25);
            this.ID.TabIndex = 0;
            this.ID.Text = "ID:";
            this.ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(58, 12);
            this.idBox.Name = "idBox";
            this.idBox.ShortcutsEnabled = false;
            this.idBox.Size = new System.Drawing.Size(114, 20);
            this.idBox.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.searchButton.Location = new System.Drawing.Point(95, 38);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(77, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // Search_By_ID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 74);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.idBox);
            this.Name = "Search_By_ID";
            this.Text = "Search_By_ID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.Button searchButton;
    }
}