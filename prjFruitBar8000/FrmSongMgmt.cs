using prjFruitBar8000.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFruitBar8000
{
    public partial class FrmSongMgmt : Form
    {
        int queryId = -1;

        public FrmSongMgmt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除?","警告",MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                queryId = int.Parse(txtIdBox.Text);
                bool isDeletedNow = (new SongDeleteService()).DeleteSong(queryId);
                if (isDeletedNow)
                {
                    MessageBox.Show("資料已成功刪除!", "警告");
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                MessageBox.Show("發生錯誤! 請洽管理員");
                return;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                queryId = int.Parse(txtIdBox.Text);
                SongQueryResult qresult = (new SongQueryService()).queryId(queryId).FirstOrDefault();

                txtAlbumName.Text = qresult.AlbumName;
                txtCreatorName.Text = qresult.ArtistNames;
                txtSongName.Text = qresult.SongName;
            }
            catch (ArgumentNullException aex)
            {
                ErrorLogger.Log(aex);
                MessageBox.Show("查無此筆資料!");
                return;
            }
            catch (FormatException fex)
            {
                ErrorLogger.Log(fex);
                MessageBox.Show("請檢查輸入格式!");
                return;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                MessageBox.Show("發生錯誤! 請洽管理員");
                return;
            }
        }

        private void btnUpdateSong_Click(object sender, EventArgs e)
        {
            string newSongName = txtSongName.Text;
            
            queryId = int.Parse(txtIdBox.Text);
            SongQueryResult qresult = (new SongQueryService()).queryId(queryId).FirstOrDefault();

            if (qresult.SongName == newSongName) return;

            bool isUpdateSucess = new SongUpdateService().UpdateSong(queryId, newSongName);
            if(isUpdateSucess)
            {
                MessageBox.Show("資料更新成功!");
                return;
            }
        }
    }
}
