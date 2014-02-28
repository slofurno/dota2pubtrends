namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoritems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "GamePlayerModel_id", "dbo.GamePlayerModels");
            DropIndex("dbo.Items", new[] { "GamePlayerModel_id" });
            CreateTable(
                "dbo.ItemNames",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        itemnumber = c.Int(nullable: false),
                        itemname = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Items", "gameplayer_id", c => c.Int());
            AlterColumn("dbo.Items", "GamePlayerModel_id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Items", "gameplayer_id", "dbo.GamePlayerModels", "id");
            CreateIndex("dbo.Items", "gameplayer_id");
            DropColumn("dbo.Items", "itemname");
            DropTable("dbo.PlayerItems");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.Items", "itemname", c => c.String());
            DropIndex("dbo.Items", new[] { "gameplayer_id" });
            DropForeignKey("dbo.Items", "gameplayer_id", "dbo.GamePlayerModels");
            AlterColumn("dbo.Items", "GamePlayerModel_id", c => c.Int());
            DropColumn("dbo.Items", "gameplayer_id");
            DropTable("dbo.ItemNames");
            CreateIndex("dbo.Items", "GamePlayerModel_id");
            AddForeignKey("dbo.Items", "GamePlayerModel_id", "dbo.GamePlayerModels", "id");
        }
    }
}
