USE [experiment-db]

IF OBJECT_ID(N'dbo.ExperimentSession', N'U') IS NOT NULL  
DROP table dbo.ExperimentSession

IF OBJECT_ID(N'dbo.Sessions', N'U') IS NOT NULL  
DROP table dbo.Sessions

IF OBJECT_ID(N'dbo.Experiments', N'U') IS NOT NULL  
DROP table dbo.Experiments

IF OBJECT_ID(N'dbo.ExperimentKeys', N'U') IS NOT NULL  
DROP table dbo.ExperimentKeys

GO
CREATE TABLE dbo.ExperimentKeys(
	Id bigint identity primary key,
	"Key" Varchar(30) NOT NULL,
	Created DATETIME NOT NULL
)

GO
CREATE TABLE dbo.Experiments(
	Id bigint identity primary key,
	Value VARCHAR(30) NOT NULL,
	Probability float NOT NULL,
	ExperimentKeyId BIGINT FOREIGN KEY REFERENCES dbo.ExperimentKeys(id) NOT NULL
)

GO
CREATE TABLE dbo.Sessions(
	Id bigint identity Primary key,
	DeviceToken Varchar(30) unique NOT NULL,
	Created DATETIME NOT NULL	
)

GO
CREATE INDEX SessionsCreatedIndex ON  dbo.Sessions(Created)

CREATE TABLE dbo.ExperimentSession(
	SessionsId BIGINT FOREIGN KEY REFERENCES dbo.Sessions(Id),
	ExperimentsId BIGINT FOREIGN KEY REFERENCES dbo.Experiments(Id),
	PRIMARY KEY (SessionsId, ExperimentsId)
)










