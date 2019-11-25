DROP TABLE [dbo].[Anchors];
DROP TABLE [dbo].[Measurements];
DROP TABLE [dbo].[Tags];
DROP TABLE [HangFire].[AggregatedCounter];
DROP TABLE [HangFire].[Counter];
DROP TABLE [HangFire].[Hash];
ALTER TABLE [HangFire].[JobParameter] DROP CONSTRAINT [FK_HangFire_JobParameter_Job];
ALTER TABLE [HangFire].[State] DROP CONSTRAINT [FK_HangFire_State_Job];
DROP TABLE [HangFire].[Job];
DROP TABLE [HangFire].[JobParameter];
DROP TABLE [HangFire].[JobQueue];
DROP TABLE [HangFire].[List];
DROP TABLE [HangFire].[Schema];
DROP TABLE [HangFire].[Server];
DROP TABLE [HangFire].[Set];
DROP TABLE [HangFire].[State];

