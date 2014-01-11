namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionUsersUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Missions", "TargetUser", "dbo.UserProfile");
            DropIndex("dbo.Missions", new[] { "TargetUser" });
            RenameColumn(table: "dbo.Missions", name: "UserId", newName: "UserMissionId");
            AddColumn("dbo.Missions", "TargetUserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Missions", "TargetUserId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.Missions", "TargetUserId");
            DropColumn("dbo.Missions", "TargetUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "TargetUser", c => c.Int(nullable: false));
            DropIndex("dbo.Missions", new[] { "TargetUserId" });
            DropForeignKey("dbo.Missions", "TargetUserId", "dbo.UserProfile");
            DropColumn("dbo.Missions", "TargetUserId");
            RenameColumn(table: "dbo.Missions", name: "UserMissionId", newName: "UserId");
            CreateIndex("dbo.Missions", "TargetUser");
            AddForeignKey("dbo.Missions", "TargetUser", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
