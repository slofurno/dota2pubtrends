namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationidchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "playerident", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "playerident", c => c.Int(nullable: false));
        }
    }
}
