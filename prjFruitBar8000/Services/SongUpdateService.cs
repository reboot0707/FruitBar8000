using prjFruitBar8000.DataEDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjFruitBar8000.Services
{
    public class SongUpdateService
    {
        public bool UpdateSong(int queryId, string newSongName)
        {
            using (FruitBarDBEntities dbcontext = new FruitBarDBEntities())
            {
                var query = dbcontext.SongsAlbums.Where(x => !x.Song.IsDeleted); //排除被軟刪除的資料
                var queryCount = query.Where(x => x.SongId == queryId).Count();

                if (queryCount == 0)
                {
                    throw new ArgumentNullException("找不到資料");
                }
                var qRawList = query.FirstOrDefault(x => x.SongId == queryId);

                string oldName = qRawList.Song.SongName;
                qRawList.Song.SongName = newSongName;
                dbcontext.SaveChanges();
                return true;
            }
        }
    }
}
