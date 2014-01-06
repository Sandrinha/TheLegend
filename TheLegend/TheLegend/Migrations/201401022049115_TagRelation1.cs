namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagRelation1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelationShips",
                c => new
                    {
                        RelationShipId = c.Int(nullable: false, identity: true),
                        UserId1 = c.Int(nullable: false),
                        UserId2 = c.Int(nullable: false),
                        TagRelationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelationShipId)
                .ForeignKey("dbo.TagRelations", t => t.TagRelationId, cascadeDelete: true)
                .Index(t => t.TagRelationId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RelationShips", new[] { "TagRelationId" });
            DropForeignKey("dbo.RelationShips", "TagRelationId", "dbo.TagRelations");
            DropTable("dbo.RelationShips");
        }
    }
}
