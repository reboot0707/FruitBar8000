using prjFruitBar8000.DataEDMX;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjFruitBar8000.Services
{
    public class SongDeleteService
    {
        public bool DeleteSong(int queryId)
        {
            using (FruitBarDBEntities dbcontext = new FruitBarDBEntities())
            {
                var queryDeleted = dbcontext.SongsAlbums.Where(x => x.Song.IsDeleted);
                var checkDeleted = queryDeleted.Where(x => x.SongId == queryId).Count();
                if (checkDeleted > 0)
                {
                    //已經被刪除了
                    return false;
                }

                var query = dbcontext.SongsAlbums.Where(x => !x.Song.IsDeleted); //排除被軟刪除的資料
                var queryCount = query.Where(x => x.SongId == queryId).Count();

                if (queryCount == 0)
                {
                    throw new ArgumentNullException("找不到資料!");
                }
                var qRawList = query.FirstOrDefault(x => x.SongId == queryId);

                qRawList.Song.IsDeleted = true;
                dbcontext.SaveChanges();
                return true;
            }
        }
    }
}
