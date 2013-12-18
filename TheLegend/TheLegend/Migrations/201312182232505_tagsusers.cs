namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagsusers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.Tags", new[] { "UserProfile_UserId" });
            CreateTable(
                "dbo.TagUserProfiles",
                c => new
                    {
                        Tag_TagID = c.Int(nullable: false),
                        UserProfile_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagID, t.UserProfile_UserId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagID, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId, cascadeDelete: true)
                .Index(t => t.Tag_TagID)
                .Index(t => t.UserProfile_UserId);
            
            DropColumn("dbo.Tags", "UserProfile_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "UserProfile_UserId", c => c.Int());
            DropIndex("dbo.TagUserProfiles", new[] { "UserProfile_UserId" });
            DropIndex("dbo.TagUserProfiles", new[] { "Tag_TagID" });
            DropForeignKey("dbo.TagUserProfiles", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.TagUserProfiles", "Tag_TagID", "dbo.Tags");
            DropTable("dbo.TagUserProfiles");
            CreateIndex("dbo.Tags", "UserProfile_UserId");
            AddForeignKey("dbo.Tags", "UserProfile_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
