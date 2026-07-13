USE [FruitBarDB];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    -------------------------------------------------------------------------
    -- 0. 匯入來源資料
    --    同一首歌曲可有多位 Artist；每位 Artist 的 CreditRoles 以分號保存。
    -------------------------------------------------------------------------
    DECLARE @Source TABLE
    (
        AlbumName   nvarchar(200) NOT NULL,
        ReleaseDate date          NOT NULL,
        TrackNumber int           NOT NULL,
        SongName    nvarchar(200) NOT NULL,
        ArtistName  nvarchar(200) NOT NULL,
        CreditRoles nvarchar(200) NULL
    );

    INSERT INTO @Source
    (
        AlbumName,
        ReleaseDate,
        TrackNumber,
        SongName,
        ArtistName,
        CreditRoles
    )
    VALUES
        (N'超かぐや姫！', '2026-01-23',  1, N'Remember', N'yuigot', N'Composer;Arranger'),
        (N'超かぐや姫！', '2026-01-23',  1, N'Remember', N'月見ヤチヨ(cv.早見沙織)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  2, N'星降る海', N'Aqu3ra', N'Lyricist;Composer;Arranger;Programming'),
        (N'超かぐや姫！', '2026-01-23',  2, N'星降る海', N'月見ヤチヨ(cv.早見沙織)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  3, N'私は、わたしの事が好き。', N'HoneyWorks', N'Arranger'),
        (N'超かぐや姫！', '2026-01-23',  3, N'私は、わたしの事が好き。', N'かぐや(cv.夏吉ゆうこ)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  4, N'ワールドイズマイン (かぐや&月見ヤチヨ ver.) [CPK! Remix]', N'ryo (supercell)', N'Lyricist;Composer;Producer;Remixer;Programming'),
        (N'超かぐや姫！', '2026-01-23',  4, N'ワールドイズマイン (かぐや&月見ヤチヨ ver.) [CPK! Remix]', N'かぐや(cv.夏吉ゆうこ)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  4, N'ワールドイズマイン (かぐや&月見ヤチヨ ver.) [CPK! Remix]', N'月見ヤチヨ(cv.早見沙織)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  5, N'Ex-Otogibanashi', N'ryo (supercell)', N'Lyricist;Composer;Producer;Programming'),
        (N'超かぐや姫！', '2026-01-23',  5, N'Ex-Otogibanashi', N'かぐや(cv.夏吉ゆうこ)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  5, N'Ex-Otogibanashi', N'月見ヤチヨ(cv.早見沙織)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  6, N'ハッピーシンセサイザ (Cover)', N'yuigot', N'Remixer'),
        (N'超かぐや姫！', '2026-01-23',  6, N'ハッピーシンセサイザ (Cover)', N'かぐや(cv.夏吉ゆうこ)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  7, N'瞬間、シンフォニー。', N'40mP', N'Lyricist;Composer;Arranger;Adapter'),
        (N'超かぐや姫！', '2026-01-23',  7, N'瞬間、シンフォニー。', N'かぐや(cv.夏吉ゆうこ)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  8, N'Reply', N'kz', N'Composer;Arranger'),
        (N'超かぐや姫！', '2026-01-23',  8, N'Reply', N'かぐや(cv.夏吉ゆうこ)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  9, N'ray (超かぐや姫！ Version)', N'TAKU INOUE', N'Arranger;Remixer'),
        (N'超かぐや姫！', '2026-01-23',  9, N'ray (超かぐや姫！ Version)', N'かぐや(cv.夏吉ゆうこ)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23',  9, N'ray (超かぐや姫！ Version)', N'月見ヤチヨ(cv.早見沙織)', N'Vocals'),
        (N'超かぐや姫！', '2026-01-23', 10, N'メルト (かぐや ver.) [CPK! Remix]', N'ryo (supercell)', N'Lyricist;Composer;Producer;Remixer;Programming'),
        (N'超かぐや姫！', '2026-01-23', 10, N'メルト (かぐや ver.) [CPK! Remix]', N'かぐや(cv.夏吉ゆうこ)', N'Vocals');

    -------------------------------------------------------------------------
    -- 1. 驗證來源資料本身
    -------------------------------------------------------------------------
    IF EXISTS
    (
        SELECT 1
        FROM @Source
        WHERE TrackNumber <= 0
           OR NULLIF(LTRIM(RTRIM(AlbumName)), N'') IS NULL
           OR NULLIF(LTRIM(RTRIM(SongName)), N'') IS NULL
           OR NULLIF(LTRIM(RTRIM(ArtistName)), N'') IS NULL
    )
    BEGIN
        ;THROW 50001, N'來源資料含有空白必要欄位或無效 TrackNumber。', 1;
    END;

    -- 同一張專輯的同一曲序只能對應一個 SongName。
    IF EXISTS
    (
        SELECT 1
        FROM @Source
        GROUP BY AlbumName, ReleaseDate, TrackNumber
        HAVING COUNT(DISTINCT SongName) > 1
    )
    BEGIN
        ;THROW 50002, N'來源資料中，同一專輯與曲序對應到多個 SongName。', 1;
    END;

    -- 同一首歌與同一 Artist 應只有一筆 credit。
    IF EXISTS
    (
        SELECT 1
        FROM @Source
        GROUP BY AlbumName, ReleaseDate, TrackNumber, SongName, ArtistName
        HAVING COUNT(*) > 1
    )
    BEGIN
        ;THROW 50003, N'來源資料含有重複的 Song-Artist credit。', 1;
    END;

    -------------------------------------------------------------------------
    -- 2. 建立或取得 Album
    --    目前 schema 沒有 AlbumName + ReleaseDate 唯一鍵，因此先防止歧義。
    -------------------------------------------------------------------------
    DECLARE @AlbumName nvarchar(200) = N'超かぐや姫！';
    DECLARE @ReleaseDate date = '2026-01-23';
    DECLARE @AlbumId int;
    DECLARE @ExistingAlbumCount int;

    SELECT @ExistingAlbumCount = COUNT(*)
    FROM Fruitbar.Albums
    WHERE AlbumName = @AlbumName
      AND ReleaseDate = @ReleaseDate
      AND IsDeleted = 0;

    IF @ExistingAlbumCount > 1
    BEGIN
        ;THROW 50004, N'資料庫已有多筆同名、同發行日且未刪除的專輯，無法判定匯入目標。', 1;
    END;

    SELECT @AlbumId = AlbumId
    FROM Fruitbar.Albums
    WHERE AlbumName = @AlbumName
      AND ReleaseDate = @ReleaseDate
      AND IsDeleted = 0;

    IF @AlbumId IS NULL
    BEGIN
        INSERT INTO Fruitbar.Albums
        (
            AlbumName,
            ReleaseDate,
            IsDeleted,
            AlbumType
        )
        VALUES
        (
            @AlbumName,
            @ReleaseDate,
            0,
            N'Album'
        );

        SET @AlbumId = CONVERT(int, SCOPE_IDENTITY());
    END;

    -------------------------------------------------------------------------
    -- 3. 建立 Artists
    --    目前暫時以 ArtistName 識別 Artist；若已有同名多筆，停止匯入。
    -------------------------------------------------------------------------
    IF EXISTS
    (
        SELECT 1
        FROM Fruitbar.Artists AS a
        INNER JOIN
        (
            SELECT DISTINCT ArtistName
            FROM @Source
        ) AS src
            ON src.ArtistName = a.ArtistName
        WHERE a.IsDeleted = 0
        GROUP BY a.ArtistName
        HAVING COUNT(*) > 1
    )
    BEGIN
        ;THROW 50005, N'資料庫已有同名且未刪除的 Artist 重複資料，無法安全匯入。', 1;
    END;

    INSERT INTO Fruitbar.Artists
    (
        ArtistName,
        IsDeleted
    )
    SELECT
        src.ArtistName,
        0
    FROM
    (
        SELECT DISTINCT ArtistName
        FROM @Source
    ) AS src
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM Fruitbar.Artists AS a
        WHERE a.ArtistName = src.ArtistName
          AND a.IsDeleted = 0
    );

    -------------------------------------------------------------------------
    -- 4. 檢查既有曲序是否與本次 SongName 衝突
    -------------------------------------------------------------------------
    IF EXISTS
    (
        SELECT 1
        FROM
        (
            SELECT DISTINCT TrackNumber, SongName
            FROM @Source
        ) AS src
        INNER JOIN Fruitbar.SongsAlbums AS sa
            ON sa.AlbumId = @AlbumId
           AND sa.TrackNumber = src.TrackNumber
        INNER JOIN Fruitbar.Songs AS s
            ON s.SongId = sa.SongId
        WHERE s.SongName <> src.SongName
    )
    BEGIN
        ;THROW 50006, N'資料庫既有曲序的 SongName 與本次來源資料不一致。', 1;
    END;

    -------------------------------------------------------------------------
    -- 5. 建立尚不存在的 Songs，再建立 SongsAlbums
    --    Songs 不以 SongName 全域去重；以本專輯的 TrackNumber 判斷是否已匯入。
    -------------------------------------------------------------------------
    DECLARE @TrackNumber int;
    DECLARE @SongName nvarchar(200);
    DECLARE @SongId int;

    DECLARE TrackCursor CURSOR LOCAL FAST_FORWARD FOR
        SELECT DISTINCT TrackNumber, SongName
        FROM @Source
        ORDER BY TrackNumber;

    OPEN TrackCursor;
    FETCH NEXT FROM TrackCursor INTO @TrackNumber, @SongName;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SongId = NULL;

        SELECT @SongId = sa.SongId
        FROM Fruitbar.SongsAlbums AS sa
        WHERE sa.AlbumId = @AlbumId
          AND sa.TrackNumber = @TrackNumber;

        IF @SongId IS NULL
        BEGIN
            INSERT INTO Fruitbar.Songs
            (
                SongName,
                IsDeleted,
                Lyrics,
                Duration
            )
            VALUES
            (
                @SongName,
                0,
                NULL,
                NULL
            );

            SET @SongId = CONVERT(int, SCOPE_IDENTITY());

            INSERT INTO Fruitbar.SongsAlbums
            (
                AlbumId,
                SongId,
                TrackNumber
            )
            VALUES
            (
                @AlbumId,
                @SongId,
                @TrackNumber
            );
        END;

        FETCH NEXT FROM TrackCursor INTO @TrackNumber, @SongName;
    END;

    CLOSE TrackCursor;
    DEALLOCATE TrackCursor;

    -------------------------------------------------------------------------
    -- 6. 新增或更新 ArtistsSongs
    --    相同 SongId + ArtistId 已有資料時，將 CreditRoles 更新為本次來源值。
    -------------------------------------------------------------------------
    UPDATE target
    SET target.CreditRoles = src.CreditRoles
    FROM Fruitbar.ArtistsSongs AS target
    INNER JOIN
    (
        SELECT
            sa.SongId,
            a.ArtistId,
            source.CreditRoles
        FROM @Source AS source
        INNER JOIN Fruitbar.SongsAlbums AS sa
            ON sa.AlbumId = @AlbumId
           AND sa.TrackNumber = source.TrackNumber
        INNER JOIN Fruitbar.Songs AS s
            ON s.SongId = sa.SongId
           AND s.SongName = source.SongName
        INNER JOIN Fruitbar.Artists AS a
            ON a.ArtistName = source.ArtistName
           AND a.IsDeleted = 0
    ) AS src
        ON src.SongId = target.SongId
       AND src.ArtistId = target.ArtistId
    WHERE ISNULL(target.CreditRoles, N'') <> ISNULL(src.CreditRoles, N'');

    INSERT INTO Fruitbar.ArtistsSongs
    (
        SongId,
        ArtistId,
        CreditRoles
    )
    SELECT
        sa.SongId,
        a.ArtistId,
        source.CreditRoles
    FROM @Source AS source
    INNER JOIN Fruitbar.SongsAlbums AS sa
        ON sa.AlbumId = @AlbumId
       AND sa.TrackNumber = source.TrackNumber
    INNER JOIN Fruitbar.Songs AS s
        ON s.SongId = sa.SongId
       AND s.SongName = source.SongName
    INNER JOIN Fruitbar.Artists AS a
        ON a.ArtistName = source.ArtistName
       AND a.IsDeleted = 0
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM Fruitbar.ArtistsSongs AS existing
        WHERE existing.SongId = sa.SongId
          AND existing.ArtistId = a.ArtistId
    );

    -------------------------------------------------------------------------
    -- 7. 完成並回傳本次專輯結果
    -------------------------------------------------------------------------
    COMMIT TRANSACTION;

    SELECT
        al.AlbumId,
        al.AlbumName,
        al.ReleaseDate,
        sa.TrackNumber,
        s.SongId,
        s.SongName,
        a.ArtistId,
        a.ArtistName,
        ars.CreditRoles
    FROM Fruitbar.Albums AS al
    INNER JOIN Fruitbar.SongsAlbums AS sa
        ON sa.AlbumId = al.AlbumId
    INNER JOIN Fruitbar.Songs AS s
        ON s.SongId = sa.SongId
    INNER JOIN Fruitbar.ArtistsSongs AS ars
        ON ars.SongId = s.SongId
    INNER JOIN Fruitbar.Artists AS a
        ON a.ArtistId = ars.ArtistId
    WHERE al.AlbumId = @AlbumId
    ORDER BY
        sa.TrackNumber,
        ars.Id;
END TRY
BEGIN CATCH
    IF CURSOR_STATUS('local', 'TrackCursor') >= 0
        CLOSE TrackCursor;

    IF CURSOR_STATUS('local', 'TrackCursor') > -3
        DEALLOCATE TrackCursor;

    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
