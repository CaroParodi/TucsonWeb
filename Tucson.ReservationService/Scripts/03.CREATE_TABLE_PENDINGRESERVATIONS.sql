USE [Tucson_Reservations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PendingReservations](
	[PendingReservationId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[ClientId] [int] NOT NULL,
	[TypeOfTable] [nvarchar](255) NOT NULL,
	[DateOfCall] [datetime] NOT NULL,
	[CategoryCode] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PendingReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


