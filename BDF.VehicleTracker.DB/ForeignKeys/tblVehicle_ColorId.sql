ALTER TABLE [dbo].[tblVehicle]
	ADD CONSTRAINT [tblVehicle_ColorId]
	FOREIGN KEY (ColorId)
	REFERENCES [dbo].[tblColor] (Id) ON DELETE CASCADE
