namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "iteminfo_id", c => c.Int());
            AddForeignKey("dbo.Items", "iteminfo_id", "dbo.ItemNames", "id");
            CreateIndex("dbo.Items", "iteminfo_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "iteminfo_id" });
            DropForeignKey("dbo.Items", "iteminfo_id", "dbo.ItemNames");
            DropColumn("dbo.Items", "iteminfo_id");
        }
    }
}
