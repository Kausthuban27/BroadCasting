CREATE TABLE [dbo].[Participants]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Participant_Name] NVARCHAR(100) NOT NULL,
	[Participant_Email] NVARCHAR(250) NOT NULL,
	[Designation] NVARCHAR(20) NOT NULL,
	[Dept_Id] INT NOT NULL,
	[Date_Of_Registration] DATETIME2(6) NOT NULL,

	CONSTRAINT [Fk_Participant_Dept_Id] FOREIGN KEY (Dept_Id) REFERENCES Department(Dept_Id) ON DELETE NO ACTION
)
