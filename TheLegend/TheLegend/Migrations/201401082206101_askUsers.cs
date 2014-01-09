namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class askUsers : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Asks", "UserAskId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Asks", "UserOriginId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Asks", "UserDestinId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.Asks", "UserAskId");
            CreateIndex("dbo.Asks", "UserOriginId");
            CreateIndex("dbo.Asks", "UserDestinId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Asks", new[] { "UserDestinId" });
            DropIndex("dbo.Asks", new[] { "UserOriginId" });
            DropIndex("dbo.Asks", new[] { "UserAskId" });
            DropForeignKey("dbo.Asks", "UserDestinId", "dbo.UserProfile");
            DropForeignKey("dbo.Asks", "UserOriginId", "dbo.UserProfile");
            DropForeignKey("dbo.Asks", "UserAskId", "dbo.UserProfile");
        }
    }
}
