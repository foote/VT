  CREATE PROCEDURE [dbo].[spGetVehicles]
AS
	SELECT v.Year,
	v.Id, v.ColorId, v.MakeId, v.ModelId, v.VIN, 
	c.Description ColorName,
	mo.Description ModelName,
	ma.Description MakeName
	FROM tblVehicle v
	INNER JOIN tblColor c on v.ColorId = c.Id
	INNER JOIN tblMake ma on v.MakeId = ma.Id
	INNER JOIN tblModel mo on v.ModelId = mo.Id

