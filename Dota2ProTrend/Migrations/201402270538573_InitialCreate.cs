namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        kills = c.Int(nullable: false),
                        deaths = c.Int(nullable: false),
                        assists = c.Int(nullable: false),
                        gpm = c.Int(nullable: false),
                        xpm = c.Int(nullable: false),
                        creepscore = c.Int(nullable: false),
                        level = c.Int(nullable: false),
                        playerslot = c.Int(nullable: false),
                        match_id = c.Int(),
                        player_id = c.Int(),
                        hero_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Matches", t => t.match_id)
                .ForeignKey("dbo.Players", t => t.player_id)
                .ForeignKey("dbo.Heroes", t => t.hero_id)
                .Index(t => t.match_id)
                .Index(t => t.player_id)
                .Index(t => t.hero_id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        playerident = c.Int(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        itemnumber = c.Int(nullable: false),
                        itemname = c.String(),
                        GamePlayerModel_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.GamePlayerModels", t => t.GamePlayerModel_id)
                .Index(t => t.GamePlayerModel_id);
            
            CreateTable(
                "dbo.SkillTimings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        time = c.Int(nullable: false),
                        skill_id = c.Int(),
                        gameplayer_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Skills", t => t.skill_id)
                .ForeignKey("dbo.GamePlayerModels", t => t.gameplayer_id)
                .Index(t => t.skill_id)
                .Index(t => t.gameplayer_id);
            
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
            DropIndex("dbo.SkillTimings", new[] { "gameplayer_id" });
            DropIndex("dbo.SkillTimings", new[] { "skill_id" });
            DropIndex("dbo.Items", new[] { "GamePlayerModel_id" });
            DropIndex("dbo.GamePlayerModels", new[] { "hero_id" });
            DropIndex("dbo.GamePlayerModels", new[] { "player_id" });
            DropIndex("dbo.GamePlayerModels", new[] { "match_id" });
            DropForeignKey("dbo.SkillTimings", "gameplayer_id", "dbo.GamePlayerModels");
            DropForeignKey("dbo.SkillTimings", "skill_id", "dbo.Skills");
            DropForeignKey("dbo.Items", "GamePlayerModel_id", "dbo.GamePlayerModels");
            DropForeignKey("dbo.GamePlayerModels", "hero_id", "dbo.Heroes");
            DropForeignKey("dbo.GamePlayerModels", "player_id", "dbo.Players");
            DropForeignKey("dbo.GamePlayerModels", "match_id", "dbo.Matches");
            DropTable("dbo.Heroes");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillTimings");
            DropTable("dbo.Items");
            DropTable("dbo.Players");
            DropTable("dbo.GamePlayerModels");
            DropTable("dbo.Matches");
        }
    }
}
