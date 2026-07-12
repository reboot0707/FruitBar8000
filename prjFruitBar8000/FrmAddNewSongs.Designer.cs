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
            this.lblSongName = new System.Windows.Forms.Label();
            this.txtSongName = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cboArtist = new System.Windows.Forms.ComboBox();
            this.lblCreatorName = new System.Windows.Forms.Label();
            this.lblAlbumName = new System.Windows.Forms.Label();
            this.cboAlbumName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblSongName
            // 
            this.lblSongName.AutoSize = true;
            this.lblSongName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblSongName.Location = new System.Drawing.Point(11, 130);
            this.lblSongName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(144, 38);
            this.lblSongName.TabIndex = 1;
            this.lblSongName.Text = "歌曲名稱:";
            // 
            // txtSongName
            // 
            this.txtSongName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.txtSongName.Location = new System.Drawing.Point(207, 126);
            this.txtSongName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSongName.Name = "txtSongName";
            this.txtSongName.Size = new System.Drawing.Size(161, 47);
            this.txtSongName.TabIndex = 4;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.btnSubmit.Location = new System.Drawing.Point(457, 300);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(128, 50);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "送出";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cboCreator
            // 
            this.cboArtist.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.cboArtist.FormattingEnabled = true;
            this.cboArtist.Location = new System.Drawing.Point(207, 74);
            this.cboArtist.Margin = new System.Windows.Forms.Padding(2);
            this.cboArtist.Name = "cboCreator";
            this.cboArtist.Size = new System.Drawing.Size(161, 46);
            this.cboArtist.TabIndex = 5;
            this.cboArtist.DropDown += new System.EventHandler(this.cboArtist_DropDown);
            // 
            // lblCreatorName
            // 
            this.lblCreatorName.AutoSize = true;
            this.lblCreatorName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblCreatorName.Location = new System.Drawing.Point(11, 82);
            this.lblCreatorName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCreatorName.Name = "lblCreatorName";
            this.lblCreatorName.Size = new System.Drawing.Size(174, 38);
            this.lblCreatorName.TabIndex = 2;
            this.lblCreatorName.Text = "創作者名稱:";
            // 
            // lblAlbumName
            // 
            this.lblAlbumName.AutoSize = true;
            this.lblAlbumName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblAlbumName.Location = new System.Drawing.Point(11, 29);
            this.lblAlbumName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAlbumName.Name = "lblAlbumName";
            this.lblAlbumName.Size = new System.Drawing.Size(144, 38);
            this.lblAlbumName.TabIndex = 2;
            this.lblAlbumName.Text = "專輯名稱:";
            // 
            // cboAlbumName
            // 
            this.cboAlbumName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.cboAlbumName.FormattingEnabled = true;
            this.cboAlbumName.Location = new System.Drawing.Point(207, 21);
            this.cboAlbumName.Margin = new System.Windows.Forms.Padding(2);
            this.cboAlbumName.Name = "cboAlbumName";
            this.cboAlbumName.Size = new System.Drawing.Size(161, 46);
            this.cboAlbumName.TabIndex = 5;
            this.cboAlbumName.DropDown += new System.EventHandler(this.cboAlbumName_DropDown);
            // 
            // FrmAddNewSongs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cboAlbumName);
            this.Controls.Add(this.cboArtist);
            this.Controls.Add(this.txtSongName);
            this.Controls.Add(this.lblAlbumName);
            this.Controls.Add(this.lblCreatorName);
            this.Controls.Add(this.lblSongName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmAddNewSongs";
            this.Text = "新增歌曲";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.TextBox txtSongName;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cboArtist;
        private System.Windows.Forms.Label lblCreatorName;
        private System.Windows.Forms.Label lblAlbumName;
        private System.Windows.Forms.ComboBox cboAlbumName;
    }
}
