namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagRelation : DbMigration
    {
        public override void Up()
        {
          
            
            CreateTable(
                "dbo.TagRelations",
                c => new
                    {
                        TagRelationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Force = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TagRelationId);
            
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TagUserProfiles", new[] { "UserProfile_UserId" });
            DropIndex("dbo.TagUserProfiles", new[] { "Tag_TagID" });
            DropIndex("dbo.Introdutions", new[] { "StateId" });
            DropIndex("dbo.Introdutions", new[] { "GameId" });
            DropIndex("dbo.Introdutions", new[] { "MissionId" });
            DropIndex("dbo.Missions", new[] { "UserId" });
            DropIndex("dbo.UserProfile", new[] { "HumorId" });
            DropForeignKey("dbo.TagUserProfiles", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.TagUserProfiles", "Tag_TagID", "dbo.Tags");
            DropForeignKey("dbo.Introdutions", "StateId", "dbo.States");
            DropForeignKey("dbo.Introdutions", "GameId", "dbo.Games");
            DropForeignKey("dbo.Introdutions", "MissionId", "dbo.Missions");
            DropForeignKey("dbo.Missions", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.UserProfile", "HumorId", "dbo.Humors");
            DropTable("dbo.TagUserProfiles");
            DropTable("dbo.TagRelations");
            DropTable("dbo.Asks");
            DropTable("dbo.Tags");
            DropTable("dbo.States");
            DropTable("dbo.Games");
            DropTable("dbo.Introdutions");
            DropTable("dbo.Missions");
            DropTable("dbo.Humors");
            DropTable("dbo.UserProfile");
        }
    }
}
