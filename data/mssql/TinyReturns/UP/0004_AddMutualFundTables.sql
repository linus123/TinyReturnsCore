IF EXISTS (SELECT name FROM sys.schemas WHERE name = N'MutualFund')
   BEGIN
      PRINT 'Dropping the DB schema'
      DROP SCHEMA [MutualFund]
END
GO
PRINT '    Creating the DB schema'
GO
CREATE SCHEMA [MutualFund] AUTHORIZATION [dbo]
GO

CREATE TABLE [MutualFund].[Events](
	[EventId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[EventType] [varchar](16) NOT NULL,
	[NewValue] [nvarchar](MAX) NOT NULL,
	[EffectiveDate] [DateTime] NOT NULL,
	[DateCreated] [DateTime] NOT NULL,
) ON [PRIMARY]

GO