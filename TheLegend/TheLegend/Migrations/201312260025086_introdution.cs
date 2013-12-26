namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class introdution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Introdutions",
                c => new
                    {
                        IntrodutionId = c.Int(nullable: false, identity: true),
                        MissionId = c.Int(nullable: false),
                        UserOriginId = c.Int(nullable: false),
                        UserDestinId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        GameResult = c.Boolean(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IntrodutionId)
                .ForeignKey("dbo.Missions", t => t.MissionId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.MissionId)
                .Index(t => t.GameId)
                .Index(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Introdutions", new[] { "StateId" });
            DropIndex("dbo.Introdutions", new[] { "GameId" });
            DropIndex("dbo.Introdutions", new[] { "MissionId" });
            DropForeignKey("dbo.Introdutions", "StateId", "dbo.States");
            DropForeignKey("dbo.Introdutions", "GameId", "dbo.Games");
            DropForeignKey("dbo.Introdutions", "MissionId", "dbo.Missions");
            DropTable("dbo.Introdutions");
        }
    }
}
