namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ask1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Asks", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Asks", "UserId");
        }
    }
}
