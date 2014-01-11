namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionTargetUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Missions", "TargetUserId", "dbo.UserProfile");
            DropIndex("dbo.Missions", new[] { "TargetUserId" });
            AddColumn("dbo.Missions", "TargetUser", c => c.Int(nullable: false));
            AddForeignKey("dbo.Missions", "TargetUser", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.Missions", "TargetUser");
            DropColumn("dbo.Missions", "TargetUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "TargetUserId", c => c.Int(nullable: false));
            DropIndex("dbo.Missions", new[] { "TargetUser" });
            DropForeignKey("dbo.Missions", "TargetUser", "dbo.UserProfile");
            DropColumn("dbo.Missions", "TargetUser");
            CreateIndex("dbo.Missions", "TargetUserId");
            AddForeignKey("dbo.Missions", "TargetUserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
