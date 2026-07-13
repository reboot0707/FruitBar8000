USE [FruitBarDB];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @AlbumId  int;
    DECLARE @ArtistId int;

    DECLARE @SongData TABLE
    (
        TrackNumber int           NOT NULL PRIMARY KEY,
        SongName    nvarchar(200) NOT NULL,
        SongId      int           NULL
    );

    INSERT INTO @SongData (TrackNumber, SongName)
    VALUES
        (1,  N'迷星叫'),
        (2,  N'壱雫空'),
        (3,  N'碧天伴走'),
        (4,  N'影色舞'),
        (5,  N'歌いましょう鳴らしましょう'),
        (6,  N'潜在表明'),
        (7,  N'音一会'),
        (8,  N'春日影(MyGO!!!!! ver.)'),
        (9,  N'詩超絆'),
        (10, N'迷路日々'),
        (11, N'無路矢'),
        (12, N'名無声'),
        (13, N'栞');

    ------------------------------------------------------------
    -- 1. 取得或建立專輯
    ------------------------------------------------------------
    SELECT TOP (1) @AlbumId = AlbumId
    FROM Fruitbar.Albums
    WHERE AlbumName = N'迷跡波'
      AND ReleaseDate = CONVERT(date, '20231101', 112)
    ORDER BY CASE WHEN IsDeleted = 0 THEN 0 ELSE 1 END, AlbumId;

    IF @AlbumId IS NULL
    BEGIN
        INSERT INTO Fruitbar.Albums
        (
            AlbumName,
            IsDeleted,
            ReleaseDate,
            AlbumType
        )
        VALUES
        (
            N'迷跡波',
            0,
            CONVERT(date, '20231101', 112),
            N'Album'
        );

        SET @AlbumId = CONVERT(int, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE Fruitbar.Albums
        SET IsDeleted = 0,
            AlbumType = COALESCE(AlbumType, N'Album')
        WHERE AlbumId = @AlbumId;
    END;

    ------------------------------------------------------------
    -- 2. 取得或建立藝術家
    ------------------------------------------------------------
    SELECT TOP (1) @ArtistId = ArtistId
    FROM Fruitbar.Artists
    WHERE ArtistName = N'MyGO!!!!!'
    ORDER BY CASE WHEN IsDeleted = 0 THEN 0 ELSE 1 END, ArtistId;

    IF @ArtistId IS NULL
    BEGIN
        INSERT INTO Fruitbar.Artists
        (
            ArtistName,
            IsDeleted,
            ArtistType
        )
        VALUES
        (
            N'MyGO!!!!!',
            0,
            N'Band'
        );

        SET @ArtistId = CONVERT(int, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE Fruitbar.Artists
        SET IsDeleted = 0,
            ArtistType = COALESCE(ArtistType, N'Band')
        WHERE ArtistId = @ArtistId;
    END;

    ------------------------------------------------------------
    -- 3. 檢查既有曲序是否與預定資料衝突
    ------------------------------------------------------------
    IF EXISTS
    (
        SELECT 1
        FROM Fruitbar.SongsAlbums AS sa
        INNER JOIN Fruitbar.Songs AS s
            ON s.SongId = sa.SongId
        INNER JOIN @SongData AS d
            ON d.TrackNumber = sa.TrackNumber
        WHERE sa.AlbumId = @AlbumId
          AND s.SongName <> d.SongName
    )
    BEGIN
        RAISERROR(N'專輯「迷跡波」已有相同曲序但不同歌曲，為避免覆寫資料已取消匯入。', 16, 1);
    END;

    IF EXISTS
    (
        SELECT 1
        FROM Fruitbar.SongsAlbums AS sa
        INNER JOIN Fruitbar.Songs AS s
            ON s.SongId = sa.SongId
        INNER JOIN @SongData AS d
            ON d.SongName = s.SongName
        WHERE sa.AlbumId = @AlbumId
          AND sa.TrackNumber <> d.TrackNumber
    )
    BEGIN
        RAISERROR(N'專輯「迷跡波」已有同名歌曲但曲序不同，為避免覆寫資料已取消匯入。', 16, 1);
    END;

    ------------------------------------------------------------
    -- 4. 取得此專輯中已正確存在的歌曲
    ------------------------------------------------------------
    UPDATE d
    SET d.SongId = sa.SongId
    FROM @SongData AS d
    INNER JOIN Fruitbar.SongsAlbums AS sa
        ON sa.AlbumId = @AlbumId
       AND sa.TrackNumber = d.TrackNumber
    INNER JOIN Fruitbar.Songs AS s
        ON s.SongId = sa.SongId
       AND s.SongName = d.SongName;

    UPDATE s
    SET s.IsDeleted = 0
    FROM Fruitbar.Songs AS s
    INNER JOIN @SongData AS d
        ON d.SongId = s.SongId
    WHERE s.IsDeleted <> 0;

    ------------------------------------------------------------
    -- 5. 對缺少的曲目建立新的 Songs 資料
    -- 不依全資料庫同名歌曲重用，避免把不同版本誤認為同一首。
    ------------------------------------------------------------
    DECLARE @InsertedSongs TABLE
    (
        SongId   int           NOT NULL,
        SongName nvarchar(200) NOT NULL
    );

    INSERT INTO Fruitbar.Songs (SongName, IsDeleted)
    OUTPUT inserted.SongId, inserted.SongName
        INTO @InsertedSongs (SongId, SongName)
    SELECT d.SongName, 0
    FROM @SongData AS d
    WHERE d.SongId IS NULL;

    UPDATE d
    SET d.SongId = i.SongId
    FROM @SongData AS d
    INNER JOIN @InsertedSongs AS i
        ON i.SongName = d.SongName
    WHERE d.SongId IS NULL;

    IF EXISTS (SELECT 1 FROM @SongData WHERE SongId IS NULL)
        RAISERROR(N'部分歌曲無法取得 SongId，已取消匯入。', 16, 1);

    ------------------------------------------------------------
    -- 6. 建立專輯曲目關聯
    ------------------------------------------------------------
    INSERT INTO Fruitbar.SongsAlbums (AlbumId, SongId, TrackNumber)
    SELECT @AlbumId, d.SongId, d.TrackNumber
    FROM @SongData AS d
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM Fruitbar.SongsAlbums AS sa
        WHERE sa.AlbumId = @AlbumId
          AND sa.SongId = d.SongId
    );

    ------------------------------------------------------------
    -- 7. 建立專輯與藝術家關聯
    ------------------------------------------------------------
    IF NOT EXISTS
    (
        SELECT 1
        FROM Fruitbar.AlbumArtist
        WHERE AlbumId = @AlbumId
          AND ArtistId = @ArtistId
    )
    BEGIN
        INSERT INTO Fruitbar.AlbumArtist (AlbumId, ArtistId)
        VALUES (@AlbumId, @ArtistId);
    END;

    ------------------------------------------------------------
    -- 8. 建立歌曲與藝術家關聯
    ------------------------------------------------------------
    INSERT INTO Fruitbar.ArtistsSongs (SongId, ArtistId)
    SELECT d.SongId, @ArtistId
    FROM @SongData AS d
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM Fruitbar.ArtistsSongs AS ars
        WHERE ars.SongId = d.SongId
          AND ars.ArtistId = @ArtistId
    );

    ------------------------------------------------------------
    -- 9. 完整性驗證
    ------------------------------------------------------------
    IF
    (
        SELECT COUNT(*)
        FROM Fruitbar.SongsAlbums
        WHERE AlbumId = @AlbumId
    ) <> 13
    BEGIN
        RAISERROR(N'專輯曲目數不是 13 筆，已取消匯入。', 16, 1);
    END;

    IF EXISTS
    (
        SELECT d.TrackNumber, d.SongName
        FROM @SongData AS d
        EXCEPT
        SELECT sa.TrackNumber, s.SongName
        FROM Fruitbar.SongsAlbums AS sa
        INNER JOIN Fruitbar.Songs AS s
            ON s.SongId = sa.SongId
        WHERE sa.AlbumId = @AlbumId
    )
    BEGIN
        RAISERROR(N'專輯曲目內容驗證失敗，已取消匯入。', 16, 1);
    END;

    COMMIT TRANSACTION;

    PRINT N'「迷跡波」、13 首曲目與 MyGO!!!!! 關聯資料建立完成。';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage nvarchar(4000) = ERROR_MESSAGE();
    RAISERROR(N'%s', 16, 1, @ErrorMessage);
END CATCH;
GO

SELECT
    a.AlbumName,
    a.AlbumType,
    a.ReleaseDate,
    sa.TrackNumber,
    s.SongName,
    ar.ArtistName,
    ar.ArtistType
FROM Fruitbar.Albums AS a
INNER JOIN Fruitbar.SongsAlbums AS sa
    ON sa.AlbumId = a.AlbumId
INNER JOIN Fruitbar.Songs AS s
    ON s.SongId = sa.SongId
LEFT JOIN Fruitbar.ArtistsSongs AS ars
    ON ars.SongId = s.SongId
LEFT JOIN Fruitbar.Artists AS ar
    ON ar.ArtistId = ars.ArtistId
WHERE a.AlbumName = N'迷跡波'
  AND a.ReleaseDate = CONVERT(date, '20231101', 112)
ORDER BY sa.TrackNumber, ar.ArtistName;
GO
