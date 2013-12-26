namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class game_name : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "Name", c => c.Int(nullable: false));
        }
    }
}
