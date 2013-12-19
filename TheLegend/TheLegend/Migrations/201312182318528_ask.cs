namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asks",
                c => new
                    {
                        AskID = c.Int(nullable: false, identity: true),
                        UserOrigin_UserId = c.Int(),
                        UserDestiny_UserId = c.Int(),
                        UserAsk_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.AskID)
                .ForeignKey("dbo.UserProfile", t => t.UserOrigin_UserId)
                .ForeignKey("dbo.UserProfile", t => t.UserDestiny_UserId)
                .ForeignKey("dbo.UserProfile", t => t.UserAsk_UserId)
                .Index(t => t.UserOrigin_UserId)
                .Index(t => t.UserDestiny_UserId)
                .Index(t => t.UserAsk_UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Asks", new[] { "UserAsk_UserId" });
            DropIndex("dbo.Asks", new[] { "UserDestiny_UserId" });
            DropIndex("dbo.Asks", new[] { "UserOrigin_UserId" });
            DropForeignKey("dbo.Asks", "UserAsk_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Asks", "UserDestiny_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Asks", "UserOrigin_UserId", "dbo.UserProfile");
            DropTable("dbo.Asks");
        }
    }
}
