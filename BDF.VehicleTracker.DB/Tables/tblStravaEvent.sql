CREATE TABLE [dbo].[tblStravaEvent]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [AspectType] varchar(10) not null,
    [EventTime] BIGINT not null,
    [ObjectId]  BIGINT not null,
    [ObjectType] VARCHAR(10) NOT NULL,
    [OwnerId] BIGINT not null,
    [SubscriptionId] INT not null
)
