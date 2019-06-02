namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            // [AspNetRoles]
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'dcb2d5f1-70f9-47a6-b78b-4418bd9d2ae9', N'CanManageMovies')");

            // [AspNetUsers]
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b599668c-3397-456e-9bd0-a108343b32a3', N'admin@vidly.com', 0, N'AJNXNutTOjbRvL9ggn5J38eptbRC6YhElkZWxlxLbvthV5G21yrjaGB37wCgaJemew==', N'1026f057-f1c3-4a7d-9205-9bc164999981', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')");
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ccafc8e1-98e5-40fa-b93e-9b8e98735716', N'guest@vidly.com', 0, N'AMi7y3sUGnLLUTtKGflBfYQ/mQ7a5BrWOv9bnjAqOwOWSU/PcwA7LBO7dzcyU7RhnA==', N'ccbb721c-7162-44bc-afb6-b45c29be9807', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')");

            // [AspNetUserRoles]
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b599668c-3397-456e-9bd0-a108343b32a3', N'dcb2d5f1-70f9-47a6-b78b-4418bd9d2ae9')");
        }
        
        public override void Down()
        {
        }
    }
}
