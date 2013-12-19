namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ask11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Asks", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Asks", "UserId", c => c.Int(nullable: false));
        }
    }
}
