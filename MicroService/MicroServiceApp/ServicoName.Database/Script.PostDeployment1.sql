/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
If NOT EXISTS (SELECT 1 FROM PESSOA WHERE ID = 1)
begin
  Insert into Pessoa(Nome, Email, Telefone) values('Maria Madalena', 'mm@gmail.com', '31 99152-5803')
end