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
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            int? queryId = null;

            try
            {
                queryId = int.Parse(txtIdBox.Text);

                //public List<SongQueryResult> queryId(int? inputId)
                SongQueryResult qresult = (new SongQueryService()).queryId(queryId).FirstOrDefault();
                txtAlbumName.Text = qresult.AlbumName;
                txtCreatorName.Text = qresult.ArtistNames;
                txtSongName.Text = qresult.SongName;
            }
            catch (Exception ex)
            {
                ErrorLogger.Show(ex);
                MessageBox.Show("發生錯誤! 請洽管理員");
            }
        }
    }
}
