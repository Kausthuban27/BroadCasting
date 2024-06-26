﻿CREATE TABLE [dbo].[Staff]
(
	[Staff_Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Staff_name] NVARCHAR(100) NOT NULL,
	[Staff_Email] NVARCHAR(250) NOT NULL,
	[Dept_Id] INT NOT NULL,
	[Sub_Id] INT NOT NULL,

	CONSTRAINT [Fk_Staff_Dept_Id] FOREIGN KEY (Dept_Id) REFERENCES Department(Dept_Id) ON DELETE NO ACTION,
	CONSTRAINT [Fk_Staff_Sub_Id] FOREIGN KEY (Sub_Id) REFERENCES Subjects(Sub_Id) ON DELETE NO ACTION
)
