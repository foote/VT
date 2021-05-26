BEGIN
	INSERT INTO dbo.tblLeaderBoard (Id, UserName, GameTime, Score)
	VALUES
	(NEWID(), 'BDF', GETDATE() - 2, 30),
	(NEWID(), 'XSD', GETDATE() - 3, 33),
	(NEWID(), 'EDT', GETDATE() - 1, 23),
	(NEWID(), 'FGT', GETDATE() - 4, 55)
END