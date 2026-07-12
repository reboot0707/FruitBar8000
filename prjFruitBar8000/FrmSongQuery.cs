using prjFruitBar8000.DataEDMX;
using prjFruitBar8000.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFruitBar8000
{
    public partial class FrmSongQuery : Form
    {
        string defaultTextboxName = "";
        FruitBarDBEntities dbcontext = null;


        public FrmSongQuery()
        {
            InitializeComponent();
            defaultTextboxName = this.txtSongName.Text;
            cbQueryType.SelectedItem = "樂曲";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cbQueryType.Text)
                {
                    case "樂曲":
                        CallQuerySongs();
                        break;

                    case "專輯":
                        CallQueryAlbums();
                        break;

                    case "創作者":
                        CallQueryArtists();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Show(ex);
            }
        }

        private void txtSongName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                btnQuery_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        public void CallQuerySongs()
        {
            SongQueryService service = new SongQueryService();
            CallQuery(service.querySongs);
        }

        public void CallQueryAlbums()
        {
            SongQueryService service = new SongQueryService();
            CallQuery(service.queryAlbums);
        }

        public void CallQueryArtists()
        {
            SongQueryService service = new SongQueryService();
            CallQuery(service.queryArtists);
        }

        public void CallQuery(Func<string, List<SongQueryResult>> queryMethod) //List<SongQueryResult> querySongs(string inputKeyword)
        {
            string keyword = this.txtSongName.Text.Trim();
            bool hasKeyword = keyword.Length > 0 && keyword != defaultTextboxName;

            if (!hasKeyword)
            {
                keyword = "";
            }

            List<SongQueryResult> qParseList = queryMethod(keyword);

            dgvSongList.DataSource = qParseList.ToList();
            dgvSongList.Columns["SongName"].HeaderText = "歌曲名稱";
            dgvSongList.Columns["AlbumName"].HeaderText = "專輯名稱";
            dgvSongList.Columns["ArtistNames"].HeaderText = "演出／製作人員";
            return;
        }
    }
}
