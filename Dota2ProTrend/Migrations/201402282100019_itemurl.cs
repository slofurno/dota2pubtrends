namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemNames", "itemurl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemNames", "itemurl");
        }
    }
}
