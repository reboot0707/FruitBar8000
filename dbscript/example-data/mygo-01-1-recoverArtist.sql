USE [FruitBarDB];
GO

SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @ArtistId int;
    DECLARE @AlbumId int;

    ------------------------------------------------------------
    -- 1. 取得或新增 Artist：MyGO!!!!!
    ------------------------------------------------------------
    SELECT @ArtistId = ArtistId
    FROM Fruitbar.Artists
    WHERE ArtistName = N'MyGO!!!!!'
      AND IsDeleted = 0;

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
    END;

    ------------------------------------------------------------
    -- 2. 找到專輯：迷跡波
    ------------------------------------------------------------
    SELECT @AlbumId = AlbumId
    FROM Fruitbar.Albums
    WHERE AlbumName = N'迷跡波'
      AND ReleaseDate = '2023-11-01'
      AND IsDeleted = 0;

    IF @AlbumId IS NULL
    BEGIN
        THROW 50001, N'找不到專輯「迷跡波」。', 1;
    END;

    ------------------------------------------------------------
    -- 3. 建立 AlbumArtist 關聯
    ------------------------------------------------------------
    IF NOT EXISTS
    (
        SELECT 1
        FROM Fruitbar.AlbumArtist
        WHERE AlbumId = @AlbumId
          AND ArtistId = @ArtistId
    )
    BEGIN
        INSERT INTO Fruitbar.AlbumArtist
        (
            AlbumId,
            ArtistId
        )
        VALUES
        (
            @AlbumId,
            @ArtistId
        );
    END;

    ------------------------------------------------------------
    -- 4. 將此專輯內所有歌曲關聯到 MyGO!!!!!
    ------------------------------------------------------------
    INSERT INTO Fruitbar.ArtistsSongs
    (
        SongId,
        ArtistId
    )
    SELECT
        sa.SongId,
        @ArtistId
    FROM Fruitbar.SongsAlbums AS sa
    WHERE sa.AlbumId = @AlbumId
      AND NOT EXISTS
      (
          SELECT 1
          FROM Fruitbar.ArtistsSongs AS artistSong
          WHERE artistSong.SongId = sa.SongId
            AND artistSong.ArtistId = @ArtistId
      );

    COMMIT TRANSACTION;

    PRINT N'MyGO!!!!! 的專輯與歌曲關聯已補齊。';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
