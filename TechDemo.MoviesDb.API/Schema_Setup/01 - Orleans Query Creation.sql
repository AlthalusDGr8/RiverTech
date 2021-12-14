
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbi].[OrleansQuery]') AND type in (N'U'))

DECLARE @current NVARCHAR(256);
DECLARE @snapshotSettings NVARCHAR(612);
BEGIN
SELECT @current = N'[' + (SELECT DB_NAME()) + N']';
SET @snapshotSettings = N'ALTER DATABASE ' + @current + N' SET READ_COMMITTED_SNAPSHOT ON; ALTER DATABASE ' + @current + N' SET ALLOW_SNAPSHOT_ISOLATION ON;';

EXECUTE sp_executesql @snapshotSettings;

-- This table defines Orleans operational queries. Orleans uses these to manage its operations,
-- these are the only queries Orleans issues to the database.
-- These can be redefined (e.g. to provide non-destructive updates) provided the stated interface principles hold.

CREATE TABLE OrleansQuery
(
	QueryKey VARCHAR(64) NOT NULL,
	QueryText VARCHAR(8000) NOT NULL,

	CONSTRAINT OrleansQuery_Key PRIMARY KEY(QueryKey)
);
END
GO