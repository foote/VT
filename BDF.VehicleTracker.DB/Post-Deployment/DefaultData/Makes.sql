BEGIN
	INSERT INTO dbo.tblMake (Id, Description)
	VALUES
	(NEWID(), 'Ford'),
	(NEWID(), 'Toyota'),
	(NEWID(), 'Chevrolet')
END
