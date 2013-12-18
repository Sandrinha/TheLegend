namespace TheLegend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class use : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Birth", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserProfile", "Sex", c => c.String());
            AddColumn("dbo.UserProfile", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Email");
            DropColumn("dbo.UserProfile", "Sex");
            DropColumn("dbo.UserProfile", "Birth");
        }
    }
}
