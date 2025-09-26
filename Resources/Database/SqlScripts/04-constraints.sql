ALTER TABLE [dbo].[EnumDescription] ADD CONSTRAINT [PK_dbo_EnumDescription]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Company] ADD CONSTRAINT [PK_dbo_Company]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Company] ADD CONSTRAINT [UQ_dbo_Company_Name]
	UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Company] ADD CONSTRAINT [UQ_dbo_Company_BusinessName]
	UNIQUE ([BusinessName])
GO

ALTER TABLE [dbo].[ThirdPartyService] ADD CONSTRAINT [PK_dbo_ThirdPartyService]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[ThirdPartyService] ADD CONSTRAINT [UQ_dbo_ThirdPartyService_EnvironmentId_Name]
	UNIQUE ([EnvironmentId], [Name])
GO

ALTER TABLE [dbo].[ThirdPartyServiceParameter] ADD CONSTRAINT [PK_dbo_ThirdPartyServiceParameter]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[ThirdPartyServiceParameter] ADD CONSTRAINT [UQ_dbo_ThirdPartyServiceParameter_ThirdPartyServiceId_Category_Name]
	UNIQUE ([ThirdPartyServiceId], [Category], [Name])
GO

ALTER TABLE [dbo].[ThirdPartyServiceParameter] ADD CONSTRAINT [FK_dbo_ThirdPartyServiceParameter_ThirdPartyServiceId_dbo_ThirdPartyService]
	FOREIGN KEY ([ThirdPartyServiceId]) REFERENCES [dbo].[ThirdPartyService]
GO

ALTER TABLE [dbo].[Responsible] ADD CONSTRAINT [PK_dbo_Responsible]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Responsible] ADD CONSTRAINT [UQ_dbo_Responsible_Email]
	UNIQUE ([Email])
GO

ALTER TABLE [dbo].[Responsible] ADD CONSTRAINT [FK_dbo_Responsible_CompanyId_dbo_Company]
	FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company]
GO

ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [PK_dbo_Branch]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [UQ_dbo_Branch_CompanyId_Name]
	UNIQUE ([CompanyId], [Name])
GO

ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [FK_dbo_Branch_CompanyId_dbo_Company]
	FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company]
GO

ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [FK_dbo_Branch_ResponsibleId_dbo_Responsible]
	FOREIGN KEY ([ResponsibleId]) REFERENCES [dbo].[Responsible]
GO

ALTER TABLE [dbo].[Pos] ADD CONSTRAINT [PK_dbo_Pos]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Pos] ADD CONSTRAINT [UQ_dbo_Pos_BranchId_Name]
	UNIQUE ([BranchId], [Name])
GO

ALTER TABLE [dbo].[Pos] ADD CONSTRAINT [FK_dbo_Pos_BranchId_dbo_Branch]
	FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch]
GO

ALTER TABLE [dbo].[InvoiceType] ADD CONSTRAINT [PK_dbo_InvoiceType]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[InvoiceType] ADD CONSTRAINT [UQ_dbo_InvoiceType_Name]
	UNIQUE ([Name])
GO

ALTER TABLE [dbo].[BranchNotification] ADD CONSTRAINT [PK_dbo_BranchNotification]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[BranchNotification] ADD CONSTRAINT [UQ_dbo_BranchNotification_BranchId_InvoiceTypeId_Email]
	UNIQUE ([BranchId], [InvoiceTypeId], [Email])
GO

ALTER TABLE [dbo].[BranchNotification] ADD CONSTRAINT [FK_dbo_BranchNotification_BranchId_dbo_Branch]
	FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch]
GO

ALTER TABLE [dbo].[BranchNotification] ADD CONSTRAINT [FK_dbo_BranchNotification_InvoiceTypeId_dbo_InvoiceType]
	FOREIGN KEY ([InvoiceTypeId]) REFERENCES [dbo].[InvoiceType]
GO

ALTER TABLE [dbo].[ContingencyReason] ADD CONSTRAINT [PK_dbo_ContingencyReason]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[ContingencyReason] ADD CONSTRAINT [UQ_dbo_ContingencyReason_Name]
	UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Contingency] ADD CONSTRAINT [PK_dbo_Contingency]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Contingency] ADD CONSTRAINT [UQ_dbo_Contingency_Name]
	UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Contingency] ADD CONSTRAINT [FK_dbo_Contingency_BranchId_dbo_Branch]
	FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch]
GO

ALTER TABLE [dbo].[Contingency] ADD CONSTRAINT [FK_dbo_Contingency_ContingencyResponsibleId_dbo_Responsible]
	FOREIGN KEY ([ContingencyResponsibleId]) REFERENCES [dbo].[Responsible]
GO

ALTER TABLE [dbo].[Contingency] ADD CONSTRAINT [FK_dbo_Contingency_ContingencyReasonId_dbo_ContingencyReason]
	FOREIGN KEY ([ContingencyReasonId]) REFERENCES [dbo].[ContingencyReason]
GO

ALTER TABLE [dbo].[ContingencyProcessingLog] ADD CONSTRAINT [PK_dbo_ContingencyProcessingLog]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[ContingencyProcessingLog] ADD CONSTRAINT [FK_dbo_ContingencyProcessingLog_ContingencyId_dbo_Contingency]
	FOREIGN KEY ([ContingencyId]) REFERENCES [dbo].[Contingency]
GO

ALTER TABLE [dbo].[Invoice] ADD CONSTRAINT [PK_dbo_Invoice]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[Invoice] ADD CONSTRAINT [UQ_dbo_Invoice_InvoiceTypeId_InvoiceNumber]
	UNIQUE ([InvoiceTypeId], [InvoiceNumber])
GO

ALTER TABLE [dbo].[Invoice] ADD CONSTRAINT [FK_dbo_Invoice_ContingencyId_dbo_Contingency]
	FOREIGN KEY ([ContingencyId]) REFERENCES [dbo].[Contingency]
GO

ALTER TABLE [dbo].[Invoice] ADD CONSTRAINT [FK_dbo_Invoice_PosId_dbo_Pos]
	FOREIGN KEY ([PosId]) REFERENCES [dbo].[Pos]
GO

ALTER TABLE [dbo].[InvoiceValidation] ADD CONSTRAINT [PK_dbo_InvoiceValidation]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[InvoiceValidation] ADD CONSTRAINT [FK_dbo_InvoiceValidation_InvoiceId_dbo_Invoice]
	FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice]
GO

ALTER TABLE [dbo].[InvoiceProcessingStatusLog] ADD CONSTRAINT [PK_dbo_InvoiceProcessingStatusLog]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[InvoiceProcessingStatusLog] ADD CONSTRAINT [FK_dbo_InvoiceProcessingStatusLog_InvoiceId_dbo_Invoice]
	FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice]
GO

ALTER TABLE [dbo].[InvoiceProcessingLog] ADD CONSTRAINT [PK_dbo_InvoiceProcessingLog]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[InvoiceProcessingLog] ADD CONSTRAINT [FK_dbo_InvoiceProcessingLog_InvoiceId_dbo_Invoice]
	FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice]
GO

ALTER TABLE [dbo].[InvoiceFile] ADD CONSTRAINT [PK_dbo_InvoiceFile]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[InvoiceFile] ADD CONSTRAINT [UQ_dbo_InvoiceFile_InvoiceId_FileName]
	UNIQUE ([InvoiceId], [FileName])
GO

ALTER TABLE [dbo].[InvoiceFile] ADD CONSTRAINT [FK_dbo_InvoiceFile_InvoiceId_dbo_Invoice]
	FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice]
GO

ALTER TABLE [dbo].[InvoiceNotification] ADD CONSTRAINT [PK_dbo_InvoiceNotification]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[InvoiceNotification] ADD CONSTRAINT [FK_dbo_InvoiceNotification_InvoiceId_dbo_Invoice]
	FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice]
GO

ALTER TABLE [dbo].[InvoiceCancellationLog] ADD CONSTRAINT [PK_dbo_InvoiceCancellationLog]
	PRIMARY KEY ([Id])
GO

ALTER TABLE [dbo].[InvoiceCancellationLog] ADD CONSTRAINT [FK_dbo_InvoiceCancellationLog_InvoiceId_dbo_Invoice]
	FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice]
GO
