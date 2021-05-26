BEGIN
	INSERT INTO dbo.tblModel (Id, Description)
	VALUES
	(NEWID(), 'Mustang'),
	(NEWID(), 'Camaro'),
	(NEWID(), 'Firebird')
END