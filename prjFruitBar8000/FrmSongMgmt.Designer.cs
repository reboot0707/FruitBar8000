namespace prjFruitBar8000
{
    partial class FrmSongMgmt
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
            this.txtIdBox = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtAlbumName = new System.Windows.Forms.TextBox();
            this.lblAlbumName = new System.Windows.Forms.Label();
            this.lblCreatorName = new System.Windows.Forms.Label();
            this.lblSongName = new System.Windows.Forms.Label();
            this.txtCreatorName = new System.Windows.Forms.TextBox();
            this.txtSongName = new System.Windows.Forms.TextBox();
            this.btnDeleteSong = new System.Windows.Forms.Button();
            this.btnUpdateSong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIdBox
            // 
            this.txtIdBox.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.txtIdBox.Location = new System.Drawing.Point(41, 25);
            this.txtIdBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIdBox.Name = "txtIdBox";
            this.txtIdBox.Size = new System.Drawing.Size(225, 34);
            this.txtIdBox.TabIndex = 1;
            this.txtIdBox.Text = "請輸入樂曲ID";
            // 
            // btnQuery
            // 
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQuery.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(299, 25);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(120, 40);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtAlbumName
            // 
            this.txtAlbumName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.txtAlbumName.Location = new System.Drawing.Point(328, 106);
            this.txtAlbumName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAlbumName.Name = "txtAlbumName";
            this.txtAlbumName.ReadOnly = true;
            this.txtAlbumName.Size = new System.Drawing.Size(652, 47);
            this.txtAlbumName.TabIndex = 9;
            // 
            // lblAlbumName
            // 
            this.lblAlbumName.AutoSize = true;
            this.lblAlbumName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblAlbumName.Location = new System.Drawing.Point(76, 118);
            this.lblAlbumName.Name = "lblAlbumName";
            this.lblAlbumName.Size = new System.Drawing.Size(144, 38);
            this.lblAlbumName.TabIndex = 7;
            this.lblAlbumName.Text = "專輯名稱:";
            // 
            // lblCreatorName
            // 
            this.lblCreatorName.AutoSize = true;
            this.lblCreatorName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblCreatorName.Location = new System.Drawing.Point(57, 188);
            this.lblCreatorName.Name = "lblCreatorName";
            this.lblCreatorName.Size = new System.Drawing.Size(174, 38);
            this.lblCreatorName.TabIndex = 8;
            this.lblCreatorName.Text = "創作者名稱:";
            // 
            // lblSongName
            // 
            this.lblSongName.AutoSize = true;
            this.lblSongName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lblSongName.Location = new System.Drawing.Point(76, 256);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(144, 38);
            this.lblSongName.TabIndex = 6;
            this.lblSongName.Text = "歌曲名稱:";
            // 
            // txtCreatorName
            // 
            this.txtCreatorName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.txtCreatorName.Location = new System.Drawing.Point(328, 176);
            this.txtCreatorName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCreatorName.Name = "txtCreatorName";
            this.txtCreatorName.ReadOnly = true;
            this.txtCreatorName.Size = new System.Drawing.Size(652, 47);
            this.txtCreatorName.TabIndex = 9;
            // 
            // txtSongName
            // 
            this.txtSongName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.txtSongName.Location = new System.Drawing.Point(328, 252);
            this.txtSongName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSongName.Name = "txtSongName";
            this.txtSongName.Size = new System.Drawing.Size(652, 47);
            this.txtSongName.TabIndex = 9;
            // 
            // btnDeleteSong
            // 
            this.btnDeleteSong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteSong.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteSong.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.btnDeleteSong.Location = new System.Drawing.Point(12, 503);
            this.btnDeleteSong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteSong.Name = "btnDeleteSong";
            this.btnDeleteSong.Size = new System.Drawing.Size(129, 48);
            this.btnDeleteSong.TabIndex = 10;
            this.btnDeleteSong.Text = "刪除";
            this.btnDeleteSong.UseVisualStyleBackColor = true;
            this.btnDeleteSong.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUpdateSong
            // 
            this.btnUpdateSong.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateSong.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.btnUpdateSong.Location = new System.Drawing.Point(439, 25);
            this.btnUpdateSong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdateSong.Name = "btnUpdateSong";
            this.btnUpdateSong.Size = new System.Drawing.Size(169, 40);
            this.btnUpdateSong.TabIndex = 10;
            this.btnUpdateSong.Text = "儲存資訊";
            this.btnUpdateSong.UseVisualStyleBackColor = true;
            this.btnUpdateSong.Click += new System.EventHandler(this.btnUpdateSong_Click);
            // 
            // FrmSongMgmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.btnUpdateSong);
            this.Controls.Add(this.btnDeleteSong);
            this.Controls.Add(this.txtSongName);
            this.Controls.Add(this.txtCreatorName);
            this.Controls.Add(this.txtAlbumName);
            this.Controls.Add(this.lblAlbumName);
            this.Controls.Add(this.lblCreatorName);
            this.Controls.Add(this.lblSongName);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtIdBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmSongMgmt";
            this.Text = "歌曲管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIdBox;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtAlbumName;
        private System.Windows.Forms.Label lblAlbumName;
        private System.Windows.Forms.Label lblCreatorName;
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.TextBox txtCreatorName;
        private System.Windows.Forms.TextBox txtSongName;
        private System.Windows.Forms.Button btnDeleteSong;
        private System.Windows.Forms.Button btnUpdateSong;
    }
}