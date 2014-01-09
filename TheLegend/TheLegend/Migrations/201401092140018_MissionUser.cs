namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "TargetUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "UserProfile_UserId", c => c.Int());
            AddForeignKey("dbo.Missions", "TargetUserId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Missions", "UserProfile_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Introdutions", "UserOriginId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Introdutions", "UserDestinId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.Missions", "TargetUserId");
            CreateIndex("dbo.Missions", "UserProfile_UserId");
            CreateIndex("dbo.Introdutions", "UserOriginId");
            CreateIndex("dbo.Introdutions", "UserDestinId");
            DropColumn("dbo.Missions", "TargetUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "TargetUser", c => c.Int(nullable: false));
            DropIndex("dbo.Introdutions", new[] { "UserDestinId" });
            DropIndex("dbo.Introdutions", new[] { "UserOriginId" });
            DropIndex("dbo.Missions", new[] { "UserProfile_UserId" });
            DropIndex("dbo.Missions", new[] { "TargetUserId" });
            DropForeignKey("dbo.Introdutions", "UserDestinId", "dbo.UserProfile");
            DropForeignKey("dbo.Introdutions", "UserOriginId", "dbo.UserProfile");
            DropForeignKey("dbo.Missions", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Missions", "TargetUserId", "dbo.UserProfile");
            DropColumn("dbo.Missions", "UserProfile_UserId");
            DropColumn("dbo.Missions", "TargetUserId");
        }
    }
}
