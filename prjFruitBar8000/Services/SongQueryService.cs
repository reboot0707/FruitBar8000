using prjFruitBar8000.DataEDMX;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjFruitBar8000.Services
{
    public class SongQueryResult
    {
        public string SongName { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
    }

    public class SongQueryService
    {
        public List<SongQueryResult> QuerySongs(string inputKeyword)
        {
            using (FruitBarDBEntities dbcontext = new FruitBarDBEntities())
            {
                var query = dbcontext.SongsAlbums.Where(x => !x.Song.IsDeleted); //排除被軟刪除的資料
                if (!string.IsNullOrWhiteSpace(inputKeyword))
                {
                    query = query.Where(x => x.Song.SongName.Contains(inputKeyword)); //查詢條件掛上要找關鍵字的情況
                }

                var qRawList = query.SelectMany(a => a.Song.ArtistsSongs,
                                               (x, a) => new 
                                               {
                                                   a.Song.SongName,
                                                   x.Album.AlbumName,
                                                   a.Artist.ArtistName
                                               }).ToList();  // 加上 ToList() 讓它從 LINQ to Entity 查詢語句轉換成可被 C# 處理(例如字串處理)的"具體"資料型態

                var qParseList = qRawList
                    .GroupBy(x => new { x.SongName, x.AlbumName })
                    .Select(g => new SongQueryResult
                    {
                        SongName = g.Key.SongName,
                        AlbumName = g.Key.AlbumName,
                        ArtistName = string.Join("; ", g.Select(y => y.ArtistName))
                    }).ToList();
                return qParseList;
            }
        }     
    }



}
