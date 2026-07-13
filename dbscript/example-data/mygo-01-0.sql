USE [FruitBarDB];
GO

SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @AlbumId int;

    ------------------------------------------------------------
    -- 1. 新增專輯：AlbumName、ReleaseDate
    ------------------------------------------------------------
    INSERT INTO Fruitbar.Albums
    (
        AlbumName,
        ReleaseDate
    )
    VALUES
    (
        N'迷跡波',
        '2023-11-01'
    );

    -- 取得剛新增專輯的 AlbumId
    SET @AlbumId = CONVERT(int, SCOPE_IDENTITY());

    ------------------------------------------------------------
    -- 2. 準備歌曲資料
    ------------------------------------------------------------
    DECLARE @SongData TABLE
    (
        TrackNumber int           NOT NULL,
        SongName    nvarchar(200) NOT NULL
    );

    INSERT INTO @SongData
    (
        TrackNumber,
        SongName
    )
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
    -- 3. 新增歌曲
    ------------------------------------------------------------
    INSERT INTO Fruitbar.Songs
    (
        SongName
    )
    SELECT
        SongName
    FROM @SongData;

    ------------------------------------------------------------
    -- 4. 建立 SongsAlbums 與 TrackNumber
    ------------------------------------------------------------
    INSERT INTO Fruitbar.SongsAlbums
    (
        AlbumId,
        SongId,
        TrackNumber
    )
    SELECT
        @AlbumId,
        s.SongId,
        d.TrackNumber
    FROM @SongData AS d
    INNER JOIN Fruitbar.Songs AS s
        ON s.SongName = d.SongName;

    COMMIT TRANSACTION;

    PRINT N'迷跡波專輯資料匯入完成。';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
