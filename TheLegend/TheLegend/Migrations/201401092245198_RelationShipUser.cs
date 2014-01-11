namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShipUser : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.RelationShips", "UserId1", "dbo.UserProfile", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.RelationShips", "UserId2", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.RelationShips", "UserId1");
            CreateIndex("dbo.RelationShips", "UserId2");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RelationShips", new[] { "UserId2" });
            DropIndex("dbo.RelationShips", new[] { "UserId1" });
            DropForeignKey("dbo.RelationShips", "UserId2", "dbo.UserProfile");
            DropForeignKey("dbo.RelationShips", "UserId1", "dbo.UserProfile");
        }
    }
}
