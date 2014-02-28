namespace Dota2ProTrend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GamePlayerModels", "match_id", "dbo.Matches");
            DropForeignKey("dbo.GamePlayerModels", "player_id", "dbo.Players");
            DropForeignKey("dbo.SkillTimings", "skill_id", "dbo.Skills");
            DropForeignKey("dbo.SkillTimings", "gameplayer_id", "dbo.GamePlayerModels");
            DropIndex("dbo.GamePlayerModels", new[] { "match_id" });
            DropIndex("dbo.GamePlayerModels", new[] { "player_id" });
            DropIndex("dbo.SkillTimings", new[] { "skill_id" });
            DropIndex("dbo.SkillTimings", new[] { "gameplayer_id" });
            RenameColumn(table: "dbo.GamePlayerModels", name: "match_id", newName: "matchid");
            RenameColumn(table: "dbo.GamePlayerModels", name: "player_id", newName: "playerid");
            RenameColumn(table: "dbo.SkillTimings", name: "skill_id", newName: "skillid");
            RenameColumn(table: "dbo.SkillTimings", name: "gameplayer_id", newName: "gameplayermodelid");
            AddForeignKey("dbo.GamePlayerModels", "matchid", "dbo.Matches", "id", cascadeDelete: true);
            AddForeignKey("dbo.GamePlayerModels", "playerid", "dbo.Players", "id", cascadeDelete: true);
            AddForeignKey("dbo.SkillTimings", "skillid", "dbo.Skills", "id", cascadeDelete: true);
            AddForeignKey("dbo.SkillTimings", "gameplayermodelid", "dbo.GamePlayerModels", "id", cascadeDelete: true);
            CreateIndex("dbo.GamePlayerModels", "matchid");
            CreateIndex("dbo.GamePlayerModels", "playerid");
            CreateIndex("dbo.SkillTimings", "skillid");
            CreateIndex("dbo.SkillTimings", "gameplayermodelid");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SkillTimings", new[] { "gameplayermodelid" });
            DropIndex("dbo.SkillTimings", new[] { "skillid" });
            DropIndex("dbo.GamePlayerModels", new[] { "playerid" });
            DropIndex("dbo.GamePlayerModels", new[] { "matchid" });
            DropForeignKey("dbo.SkillTimings", "gameplayermodelid", "dbo.GamePlayerModels");
            DropForeignKey("dbo.SkillTimings", "skillid", "dbo.Skills");
            DropForeignKey("dbo.GamePlayerModels", "playerid", "dbo.Players");
            DropForeignKey("dbo.GamePlayerModels", "matchid", "dbo.Matches");
            RenameColumn(table: "dbo.SkillTimings", name: "gameplayermodelid", newName: "gameplayer_id");
            RenameColumn(table: "dbo.SkillTimings", name: "skillid", newName: "skill_id");
            RenameColumn(table: "dbo.GamePlayerModels", name: "playerid", newName: "player_id");
            RenameColumn(table: "dbo.GamePlayerModels", name: "matchid", newName: "match_id");
            CreateIndex("dbo.SkillTimings", "gameplayer_id");
            CreateIndex("dbo.SkillTimings", "skill_id");
            CreateIndex("dbo.GamePlayerModels", "player_id");
            CreateIndex("dbo.GamePlayerModels", "match_id");
            AddForeignKey("dbo.SkillTimings", "gameplayer_id", "dbo.GamePlayerModels", "id");
            AddForeignKey("dbo.SkillTimings", "skill_id", "dbo.Skills", "id");
            AddForeignKey("dbo.GamePlayerModels", "player_id", "dbo.Players", "id");
            AddForeignKey("dbo.GamePlayerModels", "match_id", "dbo.Matches", "id");
        }
    }
}
