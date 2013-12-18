namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertags1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "UserProfile_UserId", c => c.Int());
            AddForeignKey("dbo.Tags", "UserProfile_UserId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Tags", "UserProfile_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "UserProfile_UserId" });
            DropForeignKey("dbo.Tags", "UserProfile_UserId", "dbo.UserProfile");
            DropColumn("dbo.Tags", "UserProfile_UserId");
        }
    }
}
