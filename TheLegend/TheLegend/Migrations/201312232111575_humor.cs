namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class humor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Humors",
                c => new
                    {
                        HumorId = c.Int(nullable: false, identity: true),
                        EstadoHumor = c.String(),
                    })
                .PrimaryKey(t => t.HumorId);
            
            AddColumn("dbo.UserProfile", "HumorId", c => c.Int(nullable: true));
            AddForeignKey("dbo.UserProfile", "HumorId", "dbo.Humors", "HumorId", cascadeDelete: true);
            CreateIndex("dbo.UserProfile", "HumorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserProfile", new[] { "HumorId" });
            DropForeignKey("dbo.UserProfile", "HumorId", "dbo.Humors");
            DropColumn("dbo.UserProfile", "HumorId");
            DropTable("dbo.Humors");
        }
    }
}
