using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjFruitBar8000.Services;

namespace prjFruitBar8000
{
    public partial class FrmAddNewSongs : Form
    {
        public FrmAddNewSongs()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string songName;
            string artistName;
            string albumName;
            int? successSongId = null;

            try
            {
                artistName = cboArtist.Text.Trim();
                songName = txtSongName.Text.Trim();
                albumName = cboAlbumName.Text.Trim();

                if (string.IsNullOrWhiteSpace(artistName) 
                    || string.IsNullOrWhiteSpace(songName)
                    || string.IsNullOrWhiteSpace(albumName))
                {
                    MessageBox.Show("三個欄位都要輸入!");
                    return;
                }

                SongQueryService qService = new SongQueryService();
                List<SongQueryResult> qExistSongName = qService.querySongs(songName);

                foreach (var item in qExistSongName)
                {
                    if (item.ArtistNames == artistName && item.SongName == songName && item.AlbumName == albumName)
                    {
                        MessageBox.Show("你已經加過這首歌了!");
                        return;
                    }
                }

                //ref: addSongsV1(string addSongName, string addArtistName)
                SongAddService saService = new SongAddService();
                successSongId = saService.addSongsV1(songName, artistName, albumName);
                if (successSongId > 0 && successSongId.HasValue)
                {
                    MessageBox.Show($"歌曲新增成功! 系統編號: {successSongId}");
                }
                else
                {
                    throw new Exception("歌曲新增失敗!");
                    MessageBox.Show("歌曲新增失敗! 請洽管理員");
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.Show(ex);
                MessageBox.Show("發生錯誤! 請洽管理員");
            }
        }

        private void cboAlbumName_DropDown(object sender, EventArgs e)
        {
            string qAlbumName = cboAlbumName.Text;

            //public List<SongQueryResult> queryAlbums(string inputKeyword)
            SongQueryService qService = new SongQueryService();
            var qResultList = qService.queryAlbums(qAlbumName).Select(x => x.AlbumName).ToList().Distinct();
            cboAlbumName.Items.Clear();
            foreach (var item in qResultList)
            {
                cboAlbumName.Items.Add(item);
            }
        }

        private void cboArtist_DropDown(object sender, EventArgs e)
        {
            string qArtistName = cboArtist.Text;

            //public List<SongQueryResult> queryAlbums(string inputKeyword)
            SongQueryService qService = new SongQueryService();
            var qResultList = qService.queryArtists(qArtistName).Select(x => x.ArtistNames).ToList().Distinct();
            cboAlbumName.Items.Clear();
            foreach (var item in qResultList)
            {
                cboArtist.Items.Add(item);
            }
        }
    }
}
