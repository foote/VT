ALTER TABLE [dbo].[tblVehicle]
	ADD CONSTRAINT [tblVehicle_ModelId]
	FOREIGN KEY (ModelId)
	REFERENCES [dbo].[tblModel] (Id) ON DELETE CASCADE