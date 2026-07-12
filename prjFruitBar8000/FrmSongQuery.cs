using prjFruitBar8000.DataEDMX;
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
            bool flowControl = false;
            //cbQueryType

            if(cbQueryType.Text == "樂曲")
            {
                try
                { 
                    flowControl = QuerySongs(); 
                }
                catch(Exception ex)
                {
                    ErrorLogger.Log(ex);
                    MessageBox.Show("發生錯誤, 請洽系統管理員翻閱錯誤追蹤紀錄!");
                    return;
                }
            }

            if (cbQueryType.Text == "專輯")
            {
                try
                {
                    //flowControl = QuerySongs();
                    MessageBox.Show("功能正在實作中!");
                    return;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log(ex);
                    MessageBox.Show("發生錯誤, 請洽系統管理員翻閱錯誤追蹤紀錄!");
                    return;
                }
            }

            if (cbQueryType.Text == "創作者")
            {
                try
                {
                    //flowControl = QuerySongs();
                    MessageBox.Show("功能正在實作中!");
                    return;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log(ex);
                    MessageBox.Show("發生錯誤, 請洽系統管理員翻閱錯誤追蹤紀錄!");
                    return;
                }
            }

            if (!flowControl)
            {
                return;
            }
        }

        private bool QuerySongs()
        {
            dbcontext = new FruitBarDBEntities();

            //TODO 例外處理
            if (this.txtSongName.Text == defaultTextboxName || this.txtSongName.Text.Length == 0)
            {
                var qRawList = dbcontext.SongsAlbums
                    .Where(x => !x.Song.IsDeleted)
                    .SelectMany(
                    a => a.Song.ArtistsSongs,
                    (x, a) => new
                    {
                        SongName = a.Song.SongName,
                        AlbumName = x.Album.AlbumName,
                        ArtistName = a.Artist.ArtistName
                    }
                ).ToList();
                var qParseList = qRawList.GroupBy(x => new
                {
                    x.SongName,
                    x.AlbumName
                })
                    .Select(g => new
                    {
                        g.Key.SongName,
                        g.Key.AlbumName,
                        ArtistNames = string.Join("; ", g.Select(y => y.ArtistName))
                    })
                    .ToList();
                dgvSongList.DataSource = qParseList.ToList();
                dgvSongList.Columns["SongName"].HeaderText = "歌曲名稱";
                dgvSongList.Columns["AlbumName"].HeaderText = "專輯名稱";
                dgvSongList.Columns["ArtistNames"].HeaderText = "演出／製作人員";
                return false;
            }
            else
            {
                string strSongQuery = txtSongName.Text.Trim();
                var qRawList = dbcontext.SongsAlbums
                    .Where(x => !x.Song.IsDeleted && x.Song.SongName.Contains(strSongQuery))
                    .SelectMany(
        a => a.Song.ArtistsSongs,
        (x, a) => new
        {
            SongName = a.Song.SongName,
            AlbumName = x.Album.AlbumName,
            ArtistName = a.Artist.ArtistName
        }
    ).ToList();
                var qParseList = qRawList.GroupBy(x => new
                {
                    x.SongName,
                    x.AlbumName
                })
                    .Select(g => new
                    {
                        g.Key.SongName,
                        g.Key.AlbumName,
                        ArtistNames = string.Join("; ", g.Select(y => y.ArtistName))
                    })
                    .ToList();
                dgvSongList.DataSource = qParseList.ToList();
                dgvSongList.Columns["SongName"].HeaderText = "歌曲名稱";
                dgvSongList.Columns["AlbumName"].HeaderText = "專輯名稱";
                dgvSongList.Columns["ArtistNames"].HeaderText = "演出／製作人員";
            }

            return true;
        }

        private void txtSongName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                btnQuery_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}
