namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        matchnumber = c.Int(nullable: false),
                        radiantwon = c.Boolean(nullable: false),
                        starttime = c.Int(nullable: false),
                        gamemode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GamePlayerModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        matchid = c.Int(nullable: false),
                        playerid = c.Int(nullable: false),
                        kills = c.Int(nullable: false),
                        deaths = c.Int(nullable: false),
                        assists = c.Int(nullable: false),
                        gpm = c.Int(nullable: false),
                        xpm = c.Int(nullable: false),
                        creepscore = c.Int(nullable: false),
                        level = c.Int(nullable: false),
                        playerslot = c.Int(nullable: false),
                        hero_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Matches", t => t.matchid, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.playerid, cascadeDelete: true)
                .ForeignKey("dbo.Heroes", t => t.hero_id)
                .Index(t => t.matchid)
                .Index(t => t.playerid)
                .Index(t => t.hero_id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        playerident = c.Long(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        itemnumber = c.Int(nullable: false),
                        GamePlayerModel_id = c.Int(nullable: false),
                        iteminfo_id = c.Int(),
                        gameplayer_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ItemNames", t => t.iteminfo_id)
                .ForeignKey("dbo.GamePlayerModels", t => t.gameplayer_id)
                .Index(t => t.iteminfo_id)
                .Index(t => t.gameplayer_id);
            
            CreateTable(
                "dbo.ItemNames",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        itemnumber = c.Int(nullable: false),
                        itemname = c.String(),
                        itemurl = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SkillTimings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        skillid = c.Int(nullable: false),
                        time = c.Int(nullable: false),
                        gameplayermodelid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Skills", t => t.skillid, cascadeDelete: true)
                .ForeignKey("dbo.GamePlayerModels", t => t.gameplayermodelid, cascadeDelete: true)
                .Index(t => t.skillid)
                .Index(t => t.gameplayermodelid);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        skillnumber = c.Int(nullable: false),
                        skillname = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        heronumber = c.Int(nullable: false),
                        heroname = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SkillTimings", new[] { "gameplayermodelid" });
            DropIndex("dbo.SkillTimings", new[] { "skillid" });
            DropIndex("dbo.Items", new[] { "gameplayer_id" });
            DropIndex("dbo.Items", new[] { "iteminfo_id" });
            DropIndex("dbo.GamePlayerModels", new[] { "hero_id" });
            DropIndex("dbo.GamePlayerModels", new[] { "playerid" });
            DropIndex("dbo.GamePlayerModels", new[] { "matchid" });
            DropForeignKey("dbo.SkillTimings", "gameplayermodelid", "dbo.GamePlayerModels");
            DropForeignKey("dbo.SkillTimings", "skillid", "dbo.Skills");
            DropForeignKey("dbo.Items", "gameplayer_id", "dbo.GamePlayerModels");
            DropForeignKey("dbo.Items", "iteminfo_id", "dbo.ItemNames");
            DropForeignKey("dbo.GamePlayerModels", "hero_id", "dbo.Heroes");
            DropForeignKey("dbo.GamePlayerModels", "playerid", "dbo.Players");
            DropForeignKey("dbo.GamePlayerModels", "matchid", "dbo.Matches");
            DropTable("dbo.Heroes");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillTimings");
            DropTable("dbo.ItemNames");
            DropTable("dbo.Items");
            DropTable("dbo.Players");
            DropTable("dbo.GamePlayerModels");
            DropTable("dbo.Matches");
        }
    }
}
