USE [experiment-db]

GO 
INSERT INTO dbo.Sessions
VALUES
('testSession1', GetDate())

GO
INSERT INTO dbo.ExperimentKeys
VALUES 
('button_color', GETDATE()),
('price', GETDATE())


GO
INSERT INTO dbo.Experiments
VALUES 
('#FF0000', 0.333, (SELECT ek.Id FROM dbo.ExperimentKeys ek WHERE "Key" = 'button_color')),
('#00FF00 ', 0.333, (SELECT ek.Id FROM dbo.ExperimentKeys ek WHERE "Key" = 'button_color')),
('#0000FF', 0.333, (SELECT ek.Id FROM dbo.ExperimentKeys ek WHERE "Key" = 'button_color')),
('10', 0.75, (SELECT ek.Id FROM dbo.ExperimentKeys ek WHERE "Key" = 'price')),
('20', 0.10, (SELECT ek.Id FROM dbo.ExperimentKeys ek WHERE "Key" = 'price')),
('50', 0.05, (SELECT ek.Id FROM dbo.ExperimentKeys ek WHERE "Key" = 'price')),
('5', 0.10, (SELECT ek.Id FROM dbo.ExperimentKeys ek WHERE "Key" = 'price'))



