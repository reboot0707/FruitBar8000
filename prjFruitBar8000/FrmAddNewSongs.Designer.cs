namespace prjFruitBar8000
{
    partial class FrmAddNewSongs
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
            this.lblAlbum = new System.Windows.Forms.Label();
            this.lblSongName = new System.Windows.Forms.Label();
            this.lblCreatorName = new System.Windows.Forms.Label();
            this.cboAlbum = new System.Windows.Forms.ComboBox();
            this.txtSongName = new System.Windows.Forms.TextBox();
            this.cboCreator = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblAlbum.Location = new System.Drawing.Point(6, 21);
            this.lblAlbum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(144, 38);
            this.lblAlbum.TabIndex = 0;
            this.lblAlbum.Text = "所屬專輯:";
            // 
            // lblSongName
            // 
            this.lblSongName.AutoSize = true;
            this.lblSongName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblSongName.Location = new System.Drawing.Point(6, 118);
            this.lblSongName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(144, 38);
            this.lblSongName.TabIndex = 1;
            this.lblSongName.Text = "歌曲名稱:";
            // 
            // lblCreatorName
            // 
            this.lblCreatorName.AutoSize = true;
            this.lblCreatorName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblCreatorName.Location = new System.Drawing.Point(6, 70);
            this.lblCreatorName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCreatorName.Name = "lblCreatorName";
            this.lblCreatorName.Size = new System.Drawing.Size(174, 38);
            this.lblCreatorName.TabIndex = 2;
            this.lblCreatorName.Text = "創作者名稱:";
            // 
            // cboAlbum
            // 
            this.cboAlbum.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.cboAlbum.FormattingEnabled = true;
            this.cboAlbum.Location = new System.Drawing.Point(202, 12);
            this.cboAlbum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboAlbum.Name = "cboAlbum";
            this.cboAlbum.Size = new System.Drawing.Size(161, 46);
            this.cboAlbum.TabIndex = 3;
            // 
            // txtSongName
            // 
            this.txtSongName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.txtSongName.Location = new System.Drawing.Point(202, 114);
            this.txtSongName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSongName.Name = "txtSongName";
            this.txtSongName.Size = new System.Drawing.Size(161, 47);
            this.txtSongName.TabIndex = 4;
            // 
            // cboCreator
            // 
            this.cboCreator.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.cboCreator.FormattingEnabled = true;
            this.cboCreator.Location = new System.Drawing.Point(202, 62);
            this.cboCreator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboCreator.Name = "cboCreator";
            this.cboCreator.Size = new System.Drawing.Size(161, 46);
            this.cboCreator.TabIndex = 5;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.btnSubmit.Location = new System.Drawing.Point(457, 300);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(128, 50);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "送出";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // FrmAddNewSongs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cboCreator);
            this.Controls.Add(this.txtSongName);
            this.Controls.Add(this.cboAlbum);
            this.Controls.Add(this.lblCreatorName);
            this.Controls.Add(this.lblSongName);
            this.Controls.Add(this.lblAlbum);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmAddNewSongs";
            this.Text = "新增歌曲";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAlbum;
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.Label lblCreatorName;
        private System.Windows.Forms.ComboBox cboAlbum;
        private System.Windows.Forms.TextBox txtSongName;
        private System.Windows.Forms.ComboBox cboCreator;
        private System.Windows.Forms.Button btnSubmit;
    }
}
