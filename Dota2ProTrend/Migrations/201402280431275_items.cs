namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class items : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayerItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        itemid = c.Int(nullable: false),
                        matchid = c.Int(nullable: false),
                        playerid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlayerItems");
        }
    }
}
