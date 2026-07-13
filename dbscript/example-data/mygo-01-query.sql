SELECT
    a.AlbumId,
    a.AlbumName,
    a.AlbumType,
    a.ReleaseDate,
    sa.TrackNumber,
    s.SongId,
    s.SongName,
    ar.ArtistId,
    ar.ArtistName,
    ar.ArtistType
FROM Fruitbar.Albums AS a
INNER JOIN Fruitbar.SongsAlbums AS sa
    ON a.AlbumId = sa.AlbumId
INNER JOIN Fruitbar.Songs AS s
    ON sa.SongId = s.SongId
LEFT JOIN Fruitbar.ArtistsSongs AS ars
    ON s.SongId = ars.SongId
LEFT JOIN Fruitbar.Artists AS ar
    ON ars.ArtistId = ar.ArtistId
WHERE a.AlbumName = N'迷跡波'
ORDER BY
    sa.TrackNumber,
    ar.ArtistName;
