namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AskFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Asks", "UserOrigin_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Asks", "UserDestiny_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Asks", "UserAsk_UserId", "dbo.UserProfile");
            DropIndex("dbo.Asks", new[] { "UserOrigin_UserId" });
            DropIndex("dbo.Asks", new[] { "UserDestiny_UserId" });
            DropIndex("dbo.Asks", new[] { "UserAsk_UserId" });
            AddColumn("dbo.Asks", "UserAskId", c => c.Int(nullable: false));
            AddColumn("dbo.Asks", "UserOriginId", c => c.Int(nullable: false));
            AddColumn("dbo.Asks", "UserDestinId", c => c.Int(nullable: false));
            AlterColumn("dbo.Asks", "AskId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Asks", new[] { "AskID" });
            AddPrimaryKey("dbo.Asks", "AskId");
            DropColumn("dbo.Asks", "UserOrigin_UserId");
            DropColumn("dbo.Asks", "UserDestiny_UserId");
            DropColumn("dbo.Asks", "UserAsk_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Asks", "UserAsk_UserId", c => c.Int());
            AddColumn("dbo.Asks", "UserDestiny_UserId", c => c.Int());
            AddColumn("dbo.Asks", "UserOrigin_UserId", c => c.Int());
            DropPrimaryKey("dbo.Asks", new[] { "AskId" });
            AddPrimaryKey("dbo.Asks", "AskID");
            AlterColumn("dbo.Asks", "AskID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Asks", "UserDestinId");
            DropColumn("dbo.Asks", "UserOriginId");
            DropColumn("dbo.Asks", "UserAskId");
            CreateIndex("dbo.Asks", "UserAsk_UserId");
            CreateIndex("dbo.Asks", "UserDestiny_UserId");
            CreateIndex("dbo.Asks", "UserOrigin_UserId");
            AddForeignKey("dbo.Asks", "UserAsk_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Asks", "UserDestiny_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Asks", "UserOrigin_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
