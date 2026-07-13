using prjFruitBar8000.DataEDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjFruitBar8000.Services
{
    public class SongAddService
    {
        public int? addSongsV1(string addSongName, string addArtistName, string addAlbumName)
        {
            if (string.IsNullOrWhiteSpace(addSongName))
                throw new ArgumentException("歌曲名稱不可空白");

            if (string.IsNullOrWhiteSpace(addArtistName))
                throw new ArgumentException("創作者名稱不可空白");

            if (string.IsNullOrWhiteSpace(addAlbumName))
                throw new ArgumentException("專輯名稱不可空白");


            addSongName = addSongName.Trim();
            addArtistName = addArtistName.Trim();
            addAlbumName = addAlbumName.Trim();

            using (FruitBarDBEntities dbContext = new FruitBarDBEntities())
            {
                //檢查是否有重複創作者, 歌曲重複名稱就先算了
                var artist = dbContext.Artists.FirstOrDefault(a => a.ArtistName == addArtistName && !a.IsDeleted);
                if (artist == null)
                {
                    artist = new Artist
                    {
                        ArtistName = addArtistName
                    };
                    dbContext.Artists.Add(artist);
                }

                // 查詢是否已有同名且未刪除的專輯
                var album = dbContext.Albums.FirstOrDefault(a => a.AlbumName == addAlbumName && !a.IsDeleted);
                if (album == null)
                {
                    album = new Album
                    {
                        AlbumName = addAlbumName
                    };
                    dbContext.Albums.Add(album);
                }

                var song = new Song { SongName = addSongName };
                var artistSong = new ArtistsSong { Song = song, Artist = artist, CreditRoles = null };

                // 建立歌曲與專輯的關聯。
                // TrackNumber 先給 999；之後若 UI 有輸入曲序，再改成使用 UI 傳入的值。
                var songsAlbum = new SongsAlbum { Song = song, Album = album };


                dbContext.Songs.Add(song);
                dbContext.ArtistsSongs.Add(artistSong);
                dbContext.SongsAlbums.Add(songsAlbum);
                // 把歌曲與專輯關聯加入 DbContext。

                // TODO: 先不要處理 AlbumArtist。之後處理
                // 目前目標只做到:
                // Songs -> ArtistsSongs -> Artists
                // Songs -> SongsAlbums -> Albums
                dbContext.SaveChanges();
                return song.SongId;
            }
        }
    }
}
