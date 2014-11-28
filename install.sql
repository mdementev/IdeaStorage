CREATE TABLE USERS (
UserId INT IDENTITY(1,1) NOT NULL,
FirstName NVARCHAR(100) NOT NULL,
SecondName NVARCHAR(100) NOT NULL,
Email NVARCHAR(100) NOT NULL,
Password NCHAR(44) NOT NULL,
Salt NCHAR(16) NOT NULL,
IsDeleted BIT NOT NULL,

CONSTRAINT PK_USERS PRIMARY KEY CLUSTERED (UserId ASC),
CONSTRAINT UC_Email UNIQUE (Email)
)

CREATE TABLE NODES (
NodeId INT IDENTITY(1,1) NOT NULL,
OwnerId INT NOT NULL,
Title NVARCHAR(255) NOT NULL,
Text NVARCHAR(MAX) NOT NULL,
Created DATETIME NOT NULL CONSTRAINT DF_Created DEFAULT GETDATE(),
Modified DATETIME NOT NULL CONSTRAINT DF_Modified DEFAULT GETDATE(),
IsDeleted BIT NOT NULL,

CONSTRAINT PK_NODES PRIMARY KEY CLUSTERED (NodeId ASC)
)

ALTER TABLE NODES WITH NOCHECK ADD
CONSTRAINT FK_NODES_USERS FOREIGN KEY (OwnerId) REFERENCES USERS (UserId)

CREATE TABLE TAGS (
TagId INT IDENTITY(1,1) NOT NULL,
Name NVARCHAR(100) NOT NULL,

CONSTRAINT PK_TAGS PRIMARY KEY CLUSTERED (TagId ASC),
CONSTRAINT UC_Name UNIQUE (Name)
)

CREATE TABLE TAGSETS (
TagSetId INT IDENTITY(1,1) NOT NULL,
NodeId INT NOT NULL,
TagId INT NOT NULL,

CONSTRAINT PK_TAGSETS PRIMARY KEY CLUSTERED (TagSetId ASC)
)

ALTER TABLE TAGSETS WITH NOCHECK ADD
CONSTRAINT FK_TAGSETS_NODES FOREIGN KEY (NodeId) REFERENCES NODES (NodeId)

ALTER TABLE TAGSETS WITH NOCHECK ADD
CONSTRAINT FK_TAGSETS_TAGS FOREIGN KEY (TagId) REFERENCES TAGS (TagId)